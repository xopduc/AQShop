/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('pageAddController', pageAddController);
    pageAddController.$inject = ["$scope", "apiService", "commonService", "notificationService", "$state"];

    function pageAddController($scope, apiService, commonService, notificationService, $state) {
     
        //$scope.productCategory = {
        //    CreateDate: dateNow.toISOString(),
        //    Name: "Danh mục 1",
        //    Alias: commonService.getSeoTitle("Danh mục 1"),
        //    Status: true
             

        $scope.AddPage = AddPage;
    $scope.getSeoTitle = getSeoTitle;

    $scope.ckeditorOptions = {
        languague: 'en',
        height: '200px'
    }

    function AddPage() {
        apiService.post("/api/page/create", $scope.page, function (result) {
            notificationService.displaySuccess(result.data.Name + " đã được thêm mới.");
            $state.go("pages");
        }, function () {
            notificationService.displayError("Thêm mới không thành công");
        });
    }
    
    function getSeoTitle()
    {
        $scope.page.Alias = commonService.getSeoTitle($scope.page.Name);
    }
}
    })(angular.module("aqshop.pages"));