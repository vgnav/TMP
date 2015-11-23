'use strict';

(function (window, document) {

    window.ExerciseApp = (function () {
        var _initialised = false, app = {}, _model, _tracker;                
        
        var ExerciseType = function () {
            var self = this;
            
            self.exerciseName = ko.observable();
            self.baseType = ko.observable();
            self.metricType = ko.observable();

            self.exerciseName.extend({ rateLimit: { timeout: 500, method: 'notifyWhenChangesStop' } });            
            self.exerciseName.subscribe(function (value) {
                if (!value) return;
                var showAlert = function (result) {
                    if (result) {
                        $.notify('This workout does not exist yet!', 'success');
                    } else {
                        $.notify('Workout already exists', 'error');
                    }
                };
                checkIfValidExercise(showAlert);
            });
        };

        ExerciseType.prototype.create = function () {
            var self = this;
            self.validationTriggered(true);

            var ajaxParams = getAjaxParams('/ExerciseTypes/Add');
            
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
                $.notify('Added new workout type!', 'success');
                self.validationTriggered(false);
                self.exerciseName('');
                setTimeout(function () { self.exerciseName.clearError(); }, 500);
                self.baseType('');
                self.metricType('');
                self.metricType.clearError();
            };

            var fail = function () {
                $.notify('Error creating new workout. Please try again', 'error');
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
                message: 'This workout already exists, please use another name',
                validator: function (val1, val2, callback) {       
                    checkIfValidExercise(callback);
                }
            };

            ko.validation.init({
                insertMessages: false
            });
            ko.validation.registerExtenders();
            
            self.errors = ko.validation.group(self);
            self.validationTriggered = ko.observable(false);

            self.exerciseName.extend({ required: { params: true, message: 'Workout name is required' } });
            self.exerciseName.extend({ validateExerciseName: self });

            self.metricType.extend({
                validation: {
                    message: 'Select valid exercise type',
                    validator: function () {                        
                        if (self.baseType() == 'cardio')
                            return self.metricType() == 'time' || self.metricType() == 'distance';
                        else if (self.baseType() == 'resistance')
                            return self.metricType() == 'rep' || self.metricType() == 'weight';
                        return false;
                    }
                }
            });

            self.isValidating = ko.computed(function () {
                return self.exerciseName.isValidating();
            });            
        };

        var checkIfValidExercise = function (callback) {            
            var ajaxParams = getAjaxParams('/ExerciseTypes/IsValid');

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
            showFormOnceLoaded();
        };

        return app;
    })();

    if (window.addEventListener) {
        window.addEventListener('DOMContentLoaded', window.ExerciseApp.init, false);
    }

})(window, document);