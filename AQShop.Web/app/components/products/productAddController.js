/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productAddController', productAddController);
    productAddController.$inject = ["$scope", "apiService", "commonService", "notificationService", "$state"];

    function productAddController($scope, apiService, commonService, notificationService,$state)
    {
        var dateNow = new Date();
        $scope.product= {
            CreateDate: dateNow.toISOString(),
            Name: "Sản phẩm 1",
            Alias: commonService.getSeoTitle("Sản phẩm 1"),
            Status: true
        }

        $scope.parentCategory = [];
        $scope.moreImages = [];
        $scope.getSeoTitle = getSeoTitle;

        $scope.ckeditorOptions = {
            languague: 'en',
            height: '200px'
        }

        $scope.ChooseMoreImages= function()
        {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    if ($scope.moreImages.indexOf(fileUrl) < 0) {
                        $scope.moreImages.push(fileUrl);
                    }
                })
               
            }

            finder.popup();
        }

        $scope.ChooseImage = function()
        {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl)
            {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
               
            }

            finder.popup();
        }

        function getSeoTitle()
        {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function loadCategory()
        {
            apiService.get("/api/productCategory/GetAllParentProductCategory", null, function (result) {
                $scope.parentCategory = result.data;
               
            }, function()
            {
                Console.log("can not get list parent of product");
            });
        }

        $scope.AddProduct = AddProduct;

        function AddProduct()
        {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post("/api/product/create", $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm mới.");
                $state.go("products");
            }, function () {
                notificationService.displayError("Thêm mới không thành công");
            });
        }

        loadCategory();

    }

})(angular.module("aqshop.products"));