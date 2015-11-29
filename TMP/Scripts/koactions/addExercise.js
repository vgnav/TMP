'use strict';

(function (window, document) {

    window.ExerciseApp = (function () {
        var _initialised = false, app = {}, _model, _tracker;
        var config = {
            validWorkoutName: 'This workout does not exist yet!',
            workoutExists: 'This workout already exists, please use another name or type',
            addUrl: '/ExerciseTypes/Add',
            isValidUrl: '/ExerciseTypes/IsValid',
            workoutAdded: 'Added new workout type!',
            workoutErrorAdding: 'Error creating new workout. Please try again',
            workoutNameRequired: 'Workout name is required',
            validWorkoutType: 'Select a workout type',
        }
        
        var ExerciseType = function () {
            var self = this;
            
            self.exerciseName = ko.observable();
            self.baseType = ko.observable();
            self.metricType = ko.observable();

            self.exerciseName.extend({ rateLimit: { timeout: 500, method: 'notifyWhenChangesStop' } }); 
        };

        ExerciseType.prototype.create = function () {
            var self = this;

            var ajaxParams = getAjaxParams(config.addUrl);
            
            var createAction = function (callback) {
                $.ajax(ajaxParams)
                    .done(function (response) {
                        callback(true);
                    })
                    .fail(function (response) {
                        callback(false);
                    });
            };

            var success = function () {
                toastr.success(config.workoutAdded);
                self.exerciseName('');
                // there is a timeout on this because there's a rate limiter applied to the observable
                setTimeout(function () { self.exerciseName.clearError(); }, 500);
                self.baseType('');
                self.metricType('');
                // self.metricType.clearError();
            };

            var fail = function () {
                toastr.error(config.workoutErrorAdding);
            };

            if (self.errors().length <= 0) {
                createAction(function (result) {
                    if (result) {
                        success();
                    } else {
                        fail();
                    }
                });                
            }

            return false;
        };

        ExerciseType.prototype.initValidation = function () {
            var self = this;
            // extenders
            ko.validation.rules['validateExerciseName'] = {
                async: true,
                message: config.workoutExists,
                validator: function (val1, val2, callback) {       
                    checkIfValidExercise(callback);
                }
            };

            ko.validation.init({
                insertMessages: true,
                decorateInputElement: true,
                errorElementClass: 'error',
                errorMessageClass: 'errorMsg'
            });
            ko.validation.registerExtenders();
            
            self.errors = ko.validation.group(self);

            self.exerciseName.extend({ required: { params: true, message: config.workoutNameRequired } });

            self.metricType.extend({
                validation: {
                    message: config.validWorkoutType,
                    validator: function () {                        
                        if (self.baseType() == 'cardio')
                            return self.metricType() == 'time' || self.metricType() == 'distance';
                        else if (self.baseType() == 'resistance')
                            return self.metricType() == 'rep' || self.metricType() == 'weight';
                        return false;
                    }
                },
                validateExerciseName: self
            });
            // self.exerciseName.extend({  });

            self.isValidating = ko.computed(function () {
                return self.metricType.isValidating();
            });            
        };

        var checkIfValidExercise = function (callback) {            
            var ajaxParams = getAjaxParams(config.isValidUrl);

            if (!ajaxParams.data.exerciseName) callback(true); // don't do check if no value for exerciseName
            
            $.ajax(ajaxParams)
                .done(function (response) {
                    if (callback) callback(true);
                })
                .fail(function (response) {
                    if (callback) callback(false);
                });
        };

        var getAjaxParams = function (url) {
            var ajaxParams = {
                url: url,
                type: 'POST',
                headers: { 'RequestVerificationToken': $('#requestVerificationToken').val() },
                data: {
                    exerciseName: _model.exerciseName(),
                    metricType: _model.metricType()
                }
            };
            return ajaxParams;
        };

        app.Model = null;
        app.init = function () {
            _model = new ExerciseType();
            _model.initValidation();
            app.Model = _model;
            ko.applyBindings(_model, document.getElementById('addExerciseForm'));
            TMP.Common.showFormOnceLoaded();
        };

        return app;
    })();

    if (window.addEventListener) {
        window.addEventListener('DOMContentLoaded', window.ExerciseApp.init, false);
    }

})(window, document);