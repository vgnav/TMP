'use strict';

(function (window, document) {
    window.WorkoutApp = (function () {
        var app = {}, _model;
        var config = {
            getExerciseType: '/Workout/GetWorkoutType',
            workoutNotFound: 'Could not find this workout, try another name',
            metricTypes: {
                1: 'distance',
                2: 'time',
                3: 'rep',
                4: 'weight'
            },
            confirmSetRemove: 'Are you sure you want to remove this set from the workout?',
            confirmRepRemove: 'Are you sure you want to remove this rep?'
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

        var Set = function () {
            var self = this;
            self.workoutName = ko.observable();
            self.workoutName.extend({ rateLimit: { timeout: 500, method: 'notifyWhenChangesStop' } });
            self.exerciseTypeId = ko.observable();
            self.metricType = ko.observable();
            self.exercises = ko.observableArray();

            self.toggleSet = ko.observable(true);

            self.workoutName.subscribe(function (value) {
                if (!value) {
                    // purge current Set info
                    self.exercises.removeAll();
                    return;
                }
                TMP.Common.enableForm(_model, false);
                getExerciseType(value, function (success, data) {
                    if(!success) {
                        toastr.error(config.workoutNotFound);
                        TMP.Common.enableForm(_model, true);
                        return;
                    }
                    self.exerciseTypeId(data.exerciseTypeId);
                    
                    // DEBUG ONLY - generate a random metric type since server is returning hard coded value
                    data.metricType = Math.floor(Math.random() * (5 - 1)) + 1;
                    // data.metricType = 2; 
                    // END DEBUG

                    if (self.exercises().length <= 0){
                        self.metricType(config.metricTypes[data.metricType]);
                        self.exercises.push(new Exercise(self));
                    }   
                    else {
                        self.exercises.removeAll();
                        // need to set metricType AFTER exercises have been removed otherwise UI tried to bind to the wrong exercise type
                        self.metricType(config.metricTypes[data.metricType]);
                        self.exercises.push(new Exercise(self));
                    }
                    TMP.Common.enableForm(_model, true);
                });                  
            });            
        }

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

        Workout.prototype.removeRepFromSet = function (exc) {
            if (exc.exercise.set.exercises().length <= 1) {
                alert('Can\'t remove the only rep');
                return;
            }
            var set = exc.exercise.set;
            set.exercises.remove(exc.exercise);
            // toastr.info('Rep deleted');
        };
        Workout.prototype.slideDown = function (element) {            
            $(element).hide().slideDown('fast');
        };
        Workout.prototype.slideUp = function (element) {
            $(element).slideUp('fast', function () {
                $(element).remove();
            });
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
            app.Model = _model;
            ko.applyBindings(_model, document.getElementById('workoutForm'));
            TMP.Common.showFormOnceLoaded();
        };

        return app;
    })();

    if (window.addEventListener) {
        window.addEventListener('DOMContentLoaded', window.WorkoutApp.init, false);
    }

})(window, document);