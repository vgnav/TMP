'use strict';

(function (window, document) {

    window.ExerciseApp = (function () {
        var _initialised = false, app = {}, _model, _tracker;                
        
        var ExerciseType = function () {
            var self = this;
            
            self.exerciseName = ko.observable();
            self.baseType = ko.observable();
            self.metricType = ko.observable();

            self.exerciseName.extend({ rateLimit: { timeout: 500, method: 'notifyWhenChangesStop' }});
            self.exerciseName.subscribe(function () {
                checkIfValidExercise();
            });
        };

        ExerciseType.prototype.create = function () {
            var self = this;
            
            //var payload = {
            //    exerciseName:   self.exerciseName(),
            //    metricType:     self.metricType()
            //};
            //var ajaxParams = {
            //    url: '/ExerciseTypes/Add',
            //    type: 'POST',
            //    headers: { 'RequestVerificationToken': $('#requestVerificationToken').val() },
            //    data: payload
            //};

            var ajaxParams = getAjaxParams('/ExerciseTypes/Add');
            
            $.ajax(ajaxParams)
                .done(function (response) {
                    console.log(response);
                    console.log('pass');                    
                })
                .fail(function (response) {
                    console.log(response);
                    console.log('fail');
                });
        };

        var checkIfValidExercise = function () {            
            var ajaxParams = getAjaxParams('/ExerciseTypes/IsValid');

            if (!ajaxParams.data.exerciseName) return; // don't do check if no value for exerciseName
            
            $.ajax(ajaxParams)
                .done(function (response) {
                    console.log('pass');
                })
                .fail(function (response) {
                    console.log('fail');
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
            app.Model = _model;
            ko.applyBindings(_model, document.getElementById('addExerciseForm'));
        };

        return app;
    })();

    if (window.addEventListener) {
        window.addEventListener('DOMContentLoaded', window.ExerciseApp.init, false);
    }

})(window, document);