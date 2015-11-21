

(function (window, document) {

    window.ExerciseApp = (function () {
        var _initialised = false, app = {}, _model;                
        
        var ExerciseType = function () {
            var self = this;
            
            self.exerciseName = ko.observable();
            self.baseType = ko.observable();
            self.metricType = ko.observable();
        };

        ExerciseType.prototype.create = function () {
            var self = this;
            var payload = {
                exerciseName:   self.exerciseName(),
                metricType:     self.metricType()
            };
            var ajaxParams = {
                url: '/ExerciseTypes/Add',
                type: 'POST',
                headers: { 'RequestVerificationToken': $('#requestVerificationToken').val() },
                data: payload
            };
            
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