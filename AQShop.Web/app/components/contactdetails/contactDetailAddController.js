/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('contactDetailAddController', contactDetailAddController);
    contactDetailAddController.$inject = ["$scope", "apiService", "commonService", "notificationService", "$state"];

    function contactDetailAddController($scope, apiService, commonService, notificationService, $state) {
     
        $scope.AddContactDetail = AddContactDetail;  

   

    function AddContactDetail() {
        apiService.post("/api/contactDetail/create", $scope.contactDetail, function (result) {
            notificationService.displaySuccess(result.data.Name + " đã được thêm mới.");
            $state.go("contactDetails");
        }, function () {
            notificationService.displayError("Thêm mới không thành công");
        });
    }   
   
}
    })(angular.module("aqshop.contactDetails"));