module.exports = function Customer () {
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
}

