'use strict';

(function (window, document) {
    window.WorkoutApp = (function () {
        var app = {}, _model, _allWorkouts;
        var config = {
            getExerciseType: '/Workout/GetWorkoutType',
            workoutNotFound: 'Could not find this workout, try another name',
            metricTypes: {
                1: 'distance',
                2: 'time',
                3: 'rep',
                4: 'weight'
            },
            filterMetricTypes: ['Distance', 'Time', 'Rep', 'Weight'],
            confirmSetRemove: 'Are you sure you want to remove this set from the workout?',
            confirmRepRemove: 'Are you sure you want to remove this rep?',
            cannotRemoveTheOnlyRep: 'Can\'t remove the only rep'
        };

        var Workout = function () {
            var self = this;
            self.sets = ko.observableArray();
            self.sets.push(new Set());
            self.formEnabled = ko.observable(true);
        };      

        Workout.prototype.addSet = function () {
            var self = this;
            self.sets.push(new Set());
        };
        Workout.prototype.removeRepFromSet = function (exc) {
            if (exc.exercise.set.exercises().length <= 1) {
                alert(config.cannotRemoveTheOnlyRep);
                return;
            }
            var set = exc.exercise.set;
            set.exercises.remove(exc.exercise);
            // toastr.info('Rep deleted');
        };

        var AllWorkoutTypes = function () {
            var self = this;
            self.allWorkoutTypes = ko.observableArray();
            self.filteredWorkoutTypes = ko.observableArray();
            self.selected = ko.observable();
            self.loadAllWorkouts();
            self.workoutNameFilter = ko.observable();
            self.workoutNameFilter.extend({ rateLimit: { timeout: 500, method: 'notifyWhenChangesStop' } });

            self.filterTypeOptions = config.filterMetricTypes;
            self.filterTypeSelected = ko.observable();
            self.filterType = ko.pureComputed(function () {
                return self.filterTypeOptions.indexOf(self.filterTypeSelected()) + 1;
            });

            //filter the items using the filter text
            self.filteredWorkoutTypes = ko.computed(function () {
                var nameFilter = self.workoutNameFilter() || '';
                var exerciseType = self.filterType() || 0;
                if (nameFilter.length == 0 && exerciseType == 0) {
                    return self.allWorkoutTypes();
                }
                else {
                    nameFilter = nameFilter.toLowerCase();
                    return ko.utils.arrayFilter(self.allWorkoutTypes(), function (item) {
                        if (!exerciseType) {
                            var exerciseName = item.ExerciseName.toLowerCase();
                            return TMP.Common.stringStartsWith(exerciseName, nameFilter) || TMP.Common.stringContains(exerciseName, nameFilter);
                        } else if (nameFilter.length == 0) {
                            return (+item.MetricType) == exerciseType;
                        } else {
                            var exerciseName = item.ExerciseName.toLowerCase();
                            return (TMP.Common.stringStartsWith(exerciseName, nameFilter) || TMP.Common.stringContains(exerciseName, nameFilter)) && ((+item.MetricType) == exerciseType);
                        }
                        return false;
                    });
                }
            });
            self.loadPagination();
        };

        AllWorkoutTypes.prototype.loadPagination = function () {
            var self = this;

            // pagination
            self.pageNumber = ko.observable(0);
            self.nbPerPage = 5;
            self.threshold = 3;
            self.totalPagesHolder = ko.observableArray([]);
            self.totalPages = ko.computed(function () {
                var div = Math.floor(self.filteredWorkoutTypes().length / self.nbPerPage);
                div += self.filteredWorkoutTypes().length % self.nbPerPage > 0 ? 1 : 0;
                var pages = div - 1;

                self.totalPagesHolder.removeAll();
                var totalPages = pages + 1;

                if (totalPages > self.threshold) {
                    var start = self.pageNumber();
                    if (start == 0)++start;
                    if (start > totalPages)--start;

                    for (var i = 0; i < self.threshold; i++) {
                        self.totalPagesHolder.push(start + i);
                        if (start + 1 + i > totalPages)
                            break;
                    }
                    if (start + self.threshold <= totalPages)
                        self.totalPagesHolder.push(pages + 1);

                } else {
                    for (var i = 0; i < totalPages; i++) {
                        self.totalPagesHolder.push(i + 1);
                    }
                }
                return div - 1;
            });
            self.paginated = ko.computed(function () {
                var first = self.pageNumber() * self.nbPerPage;
                return self.filteredWorkoutTypes().slice(first, first + self.nbPerPage);
            });
            self.hasPrevious = ko.computed(function () {
                return self.pageNumber() !== 0;
            });
            self.hasNext = ko.computed(function () {
                return self.pageNumber() !== self.totalPages();
            });
            self.next = function () {
                if (self.pageNumber() < self.totalPages()) {
                    self.pageNumber(self.pageNumber() + 1);
                }
            };
            self.previous = function () {
                if (self.pageNumber() != 0) {
                    self.pageNumber(self.pageNumber() - 1);
                }
            };
            self.pageController = function (targetPage) {
                return self.pageNumber(targetPage - 1);
            };
        }
        AllWorkoutTypes.prototype.loadAllWorkouts = function () {
            var self = this;
            for (var i in AllWorkouts) {
                var workout = AllWorkouts[i];
                self.allWorkoutTypes.push(workout);
            }
        };
        AllWorkoutTypes.prototype.selectItem = function (el) {
            _allWorkouts.selected(el);
        };

        var Set = function () {
            var self = this;
            self.workoutSelected = ko.observable(null);
            self.workoutName = ko.observable();
            //self.workoutName.extend({ rateLimit: { timeout: 500, method: 'notifyWhenChangesStop' } });
            self.exerciseTypeId = ko.observable();
            self.metricType = ko.observable();
            self.exercises = ko.observableArray();

            self.toggleSet = ko.observable(true);

            self.workoutSelected.subscribe(function (workout) {
                if (!workout) {
                    // purge current Set info
                    self.exercises.removeAll();
                    return;
                }

                self.workoutName(workout.ExerciseName);
                if (self.exercises().length <= 0){
                    self.metricType(config.metricTypes[workout.MetricType]);
                    self.exercises.push(new Exercise(self));
                }   
                else {
                    self.exercises.removeAll();
                    // need to set metricType AFTER exercises have been removed otherwise UI tries to bind to the wrong exercise type
                    self.metricType(config.metricTypes[workout.MetricType]);
                    self.exercises.push(new Exercise(self));
                }

                //TMP.Common.enableForm(_model, false);
                //getExerciseType(value, function (success, data) {
                //    if(!success) {
                //        toastr.error(config.workoutNotFound);
                //        TMP.Common.enableForm(_model, true);
                //        return;
                //    }
                //    self.exerciseTypeId(data.exerciseTypeId);
                    
                //    // DEBUG ONLY - generate a random metric type since server is returning hard coded value
                //    data.metricType = Math.floor(Math.random() * (5 - 1)) + 1;
                //    // data.metricType = 2; 
                //    // END DEBUG

                //    if (self.exercises().length <= 0){
                //        self.metricType(config.metricTypes[data.metricType]);
                //        self.exercises.push(new Exercise(self));
                //    }   
                //    else {
                //        self.exercises.removeAll();
                //        // need to set metricType AFTER exercises have been removed otherwise UI tries to bind to the wrong exercise type
                //        self.metricType(config.metricTypes[data.metricType]);
                //        self.exercises.push(new Exercise(self));
                //    }
                //    TMP.Common.enableForm(_model, true);
                //});                  
            });            
        }

        Set.prototype.selectWorkout = function (currentSet) {
            var self = this;
            var modal = $('.workoutPicker').remodal();
            // the event below should only fire once: when the 'workout name' text is selected on a particular set
            // however, if we dont 'unbind' the event handler once its done its work, the binding from call 1 and call 2
            // will both be called
            $(document).on('closed', '.workoutPicker', function (e) {
                $(document).unbind('closed');
                var selected = _allWorkouts.selected();                
                if (!selected || selected.ExerciseName == currentSet.workoutName()) return;
                currentSet.workoutSelected(selected);
                _allWorkouts.selected(null);
            });
            modal.open();            
        };
        Set.prototype.addRep = function () {
            var self = this;
            var prev = null;
            // todo: when adding, copy the last exercise and push
            self.exercises.push(new Exercise(self));
            self.toggleSet(true);
        };
        Set.prototype.toggleVisible = function () {
            this.toggleSet(!this.toggleSet());
        };
        Set.prototype.remove = function (set) {
            if (window.confirm(config.confirmSetRemove)) {
                _model.sets.remove(set);
            }
        };

        var Exercise = function (set) {
            var self = this;
            self.set = set;
            var func = metricTemplates[set.metricType()]; // find the right function for the metric
            self.metric = ko.observable(new func(self)); // create new instance of metric so it can be recorded
        };

        var _idCount = 0;
        function getNewId(prefix) {
            return prefix + '_' + (++_idCount) + '_';
        }

        var TimeMetric = function (exercise) {
            var self = this;
            self.exercise = exercise;
            self.time = ko.observable();
            self.calories = ko.observable();

            self.minutes = ko.observable();
            self.seconds = ko.observable();

            self.unit = ko.observable('second');

            self.elId = getNewId('time');
        };
        var DistanceMetric = function (exercise) {
            var self = this;
            self.exercise = exercise;
            self.distance = ko.observable();
            self.calories = ko.observable();

            self.unit = ko.observable('km');
            self.unit.subscribe(function (value) {
                if (!value) return;
                if (!self.distance()) return;
                if (value == 'km') {
                    self.distance((self.distance() / 0.621371).toFixed(2));
                } else if (value == 'mi') {
                    self.distance((self.distance() * 0.621371).toFixed(2));
                }
            });

            self.elId = getNewId('distance');
        };
        var RepMetric = function (exercise) {
            var self = this;
            self.exercise = exercise;
            self.reps = ko.observable();

            self.elId = getNewId('rep');
        };
        var WeightMetric = function (exercise) {
            var self = this;
            self.exercise = exercise;
            self.reps = ko.observable();
            self.weight = ko.observable();

            self.elId = getNewId('weight');

            self.unit = ko.observable('kg');
            self.unit.subscribe(function (value) {
                if (!value) return;
                if (!self.weight()) return;
                if (value == 'kg') {
                    self.weight((self.weight() / 2.20462).toFixed(2));
                } else if (value == 'lb') {
                    self.weight((self.weight() * 2.20462).toFixed(2));
                }
            });
        };

        var metricTemplates = {
            'distance': DistanceMetric,
            'time': TimeMetric,
            'rep': RepMetric,
            'weight': WeightMetric
        };

        var getExerciseType = function(exerciseName, callback) {
            var params = getAjaxParams(config.getExerciseType, exerciseName, 'GET');
            $.ajax(params)
                .done(function(res) { 
                    callback(true, res); 
                })
                .fail(function(res) { 
                    callback(false, res); 
                });
        }

        var getAjaxParams = function (url, data, type) {
            var ajaxParams = {
                url: url,
                type: type || 'POST',
                headers: { 'RequestVerificationToken': $('#requestVerificationToken').val() },
                data: data
            };
            return ajaxParams;
        };

        app.Model = null;
        app.init = function () {
            _model = new Workout();
            _allWorkouts = new AllWorkoutTypes();
            app.Model = _model;
            ko.applyBindings(_model, document.getElementById('workoutForm'));
            ko.applyBindings(_allWorkouts, document.getElementsByClassName('workoutPicker')[0]);
            TMP.Common.showFormOnceLoaded();
        };

        return app;
    })();

    if (window.addEventListener) {
        window.addEventListener('DOMContentLoaded', window.WorkoutApp.init, false);
    }

})(window, document);