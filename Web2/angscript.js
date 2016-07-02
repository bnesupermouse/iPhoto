	// create the module and name it scotchApp
	var scotchApp = angular.module('scotchApp', ['ngRoute']);

	// configure our routes
	scotchApp.config(function($routeProvider) {
		$routeProvider

			// route for the home page
			.when('/', {
				templateUrl : 'pages/home.html',
				controller  : 'mainController'
			})
            .when('/account', {
				templateUrl : 'pages/account.html',
				controller  : 'accountController'
			})
            .when('/signin', {
				templateUrl : 'pages/signin.html',
				controller  : 'signinController'
			})
			// route for the about page
			.when('/signup', {
				templateUrl : 'pages/signup.html',
				controller  : 'signupController'
			});
	});

	// create the controller and inject Angular's $scope
	scotchApp.controller('mainController', function($scope) {
		// create a message to display in our view
		
	});

	scotchApp.controller('accountController', function($scope) {
		
	});
    scotchApp.controller('signinController', function($scope) {
		
	});	

	scotchApp.controller('signupController', ['$scope', '$http', function($scope, $http) {  
    var bookId = 1;

    $scope.addNewAccount = function() {
    var ctm = new function(){
     this.CustomerId = 0;
     this.Email = "";
     this.CustomerName = "";
     this.Password = "";
     this.Gender = 0;
     this.Age;
     this.Address = "";
     this.Phone = "";
     this.OpenDate = new Date().toJSON();
     this.LastLoginTime = new Date().toJSON();
     this.Status = 0;
     };

		ctm.CustomerName = $scope.CustomerName;
		ctm.Email = $scope.Email;
		ctm.Password = $scope.Password;
		var parameter = JSON.stringify(ctm);
        $http.post('http://localhost:9000/api/customer/newaccount', parameter);
    };
}]);

	