'use strict';

// start-up functions
(function (window, document) {
    window.Core = (function () {
        var app = {};

        app.init = function () {
            $.notify.defaults({
                clickToHide: true,
                autoHideDelay: 2000,
                globalPosition: 'top left'
            });
        };

        return app;
    })();

    if (window.addEventListener) {
        window.addEventListener('DOMContentLoaded', window.Core.init, false);
    }

})(window, document);


var TrackView = function (model) {
    var self = this;
    self.changeLog = new Array();
    self.model = model;

    self.catchChanges = function (prop, val) {
        self.changeLog.push({ property: prop, value: val });
        self.model['isDirty'] = true;
    };

    self.init = function () {
        var createSubscription = function (property) {
            self.model[property].subscribe(function (value) {
                self.catchChanges(property, value);
            });
        };

        for (var property in self.model) {
            if (self.model.hasOwnProperty(property)) {
                if (self.model[property].subscribe) {
                    // calling a separate method here due to closure
                    createSubscription(property);
                }
            }
        }

        self.model['isDirty'] = false;
    };

    self.reset = function () {
        self.changeLog = new Array();
        self.model['isDirty'] = false;
    };
};

function showFormOnceLoaded(form, loadingEl) {
    if (!form) form = '.form';
    if (!loadingEl) loadingEl = '.formLoading';

    $(loadingEl).fadeOut(function () {
        $(form).fadeIn();
    });
};