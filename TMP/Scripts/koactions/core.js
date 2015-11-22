'use strict';


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