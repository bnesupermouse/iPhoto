module Controllers {
    export class MainPageCtrl {
        $scope: ng.IScope;

        constructor($scope: ng.IScope) {
            var self = this;

            self.$scope = $scope;
            self.init();
        }

        private init(): void {
            var self = this;
        }
    }
}
