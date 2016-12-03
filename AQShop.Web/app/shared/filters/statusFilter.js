/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.filter('statusFilter', function () {
        return function(input)
        {
            if (input == 1)
                return "kích hoạt";
            return "Khóa";
        }
    })
})(angular.module('aqshop.common'));