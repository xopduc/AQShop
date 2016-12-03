/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);
    productCategoryAddController.$inject = ["$scope", "apiService", "commonService", "notificationService", "$state"];

    function productCategoryAddController($scope, apiService, commonService, notificationService,$state)
    {
        var dateNow = new Date();
        $scope.productCategory= {
            CreateDate: dateNow.toISOString(),
            Name: "Danh mục 1",
            Alias: commonService.getSeoTitle("Danh mục 1"),
            Status: true
        }

        $scope.parentCategory = [];
        $scope.getSeoTitle = getSeoTitle;

        function getSeoTitle()
        {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function loadParentCategory()
        {
            apiService.get("/api/productCategory/GetAllParentProductCategory", null, function(result) {
                $scope.parentCategory = result.data;
               
            }, function()
            {
                Console.log("can not get list parent of product category");
            });
        }

        $scope.AddProductCategory = AddProductCategory;

        function AddProductCategory()
        {
            apiService.post("/api/productCategory/create", $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm mới.");
                $state.go("product_categories");
            }, function () {
                notificationService.displayError("Thêm mới không thành công");
            });
        }

        loadParentCategory();

    }

})(angular.module("aqshop.product_categories"));