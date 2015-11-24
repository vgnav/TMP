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
            confirmSetRemove: 'Are you sure you want to remove this set from the workout?'
        };

        var Workout = function () {
            var self = this;
            self.sets = ko.observableArray();
            self.sets.push(new Set());
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
                    return;
                }
                getExerciseType(value, function(success, data) {
                    if(!success) {
                        toastr.error(config.workoutNotFound);
                        return;
                    }
                    self.exerciseTypeId(data.exerciseTypeId);

                    // DEBUG ONLY - generate a random metric type since server is returning hard coded value
                    data.metricType = Math.floor(Math.random() * (5 - 1)) + 1;
                    // data.metricType = 2; // distance
                    // END DEBUG

                    self.metricType(config.metricTypes[data.metricType]);

                    if (self.exercises().length <= 0)
                        self.exercises.push(new Exercise(self));
                    else {
                        self.exercises([]);
                        self.exercises.push(new Exercise(self));
                    }
                });                  
            });            
        }

        Set.prototype.addRep = function () {
            var self = this;
            var prev = null;
            self.exercises.push(new Exercise(self));
            self.toggleSet(true);
        };

        Set.prototype.toggleVisible = function () {
            this.toggleSet(!this.toggleSet());
        }

        Set.prototype.remove = function (set) {
            if (window.confirm(config.confirmSetRemove)) {
                _model.sets.remove(set);
            }            
        }

        var Exercise = function (set) {
            var self = this;
            self.set = set;
            var func = metricTemplates[set.metricType()]; // find the right function for the metric
            self.metric = ko.observable(new func(self)); // create new instance of metric so it can be recorded
        };

            var TimeMetric = function (exercise) {
                var self = this;
                self.exercise = exercise;
                self.time = ko.observable();
                self.calories = ko.observable();
                // TODO: units
                self.elId = 'time_' + _model.sets().length + "_" + exercise.set.exercises().length;
            };

            var DistanceMetric = function (exercise) {
                var self = this;
                self.exercise = exercise;
                self.distance = ko.observable();
                self.calories = ko.observable();
                self.elId = 'distance_' + _model.sets().length + "_" + exercise.set.exercises().length;
            };

            var RepMetric = function (exercise) {
                var self = this;
                self.exercise = exercise;
                self.reps = ko.observable();
                self.elId = 'rep_' + _model.sets().length + "_" + exercise.set.exercises().length;
            };

            var WeightMetric = function (exercise) {
                var self = this;
                self.exercise = exercise;
                self.reps = ko.observable();
                self.weight = ko.observable();
                // TODO: units
                self.elId = 'weight_' + _model.sets().length + "_" + exercise.set.exercises().length;
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