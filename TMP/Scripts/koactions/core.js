'use strict';

var TMP = {};

TMP.Common = {};

TMP.Common.showFormOnceLoaded = function (form, loadingEl) {
    // Defaults
    if (!form) form = '.form';
    if (!loadingEl) loadingEl = '.formLoading';

    $(loadingEl).fadeOut(function () {
        $(form).fadeIn();
    });
};

TMP.Common.logViewChanges = function (model) {
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

// start-up functions. Need to do it like this because can't guarantee that JQuery is loaded...
(function (window, document) {
    window.appInit = (function () {
        var app = {};
        app.startUp = function () {
            toastr.options = {
                'closeButton': false,
                'progressBar': false,
                'positionClass': 'toast-top-left',
                'preventDuplicates': true,
                'showDuration': '300',
                'hideDuration': '1000',
                'timeOut': '2000',
                'extendedTimeOut': '1000',
                'showEasing': 'swing',
                'hideEasing': 'linear',
                'showMethod': 'fadeIn',
                'hideMethod': 'fadeOut',
                'newestOnTop': true
            }
        };

        return app;
    })();

    if (window.addEventListener) {
        window.addEventListener('DOMContentLoaded', window.appInit.startUp, false);
    }

})(window, document);



