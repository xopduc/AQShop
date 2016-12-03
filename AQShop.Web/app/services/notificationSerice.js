(function (app) {
    app.factory('notificationService', notificationService);
  
    function notificationService()
    {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 500,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        };
        
        return {
            displaySuccess: displaySuccess,
            displayError: displayError,          
            displayWarning: displayWarning,
            displayInfo: displayInfo
        }

        function displayError(error) {
            if (Array.isArray(error)) {
                error.each(function (err) {
                    toastr.error(err);
                });
            }
            else {
                toastr.error(error);
            }
        }

        function displaySuccess(message) {
            toastr.success(message);
        }

        function displayWarning(warning) {
            toastr.warning(warning);
        }

        function displayInfo(info) {
            toastr.info(info);
        }

        
    }

       
    
})(angular.module("aqshop.common"));