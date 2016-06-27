var UpdCustomer = function()
{
    var Customer = require('./Customer.js');
    this.OldCustomer = null;
    this.NewCustomer = null;
    this.Action = 1;
    this.TxnId = 1;
}
module.exports = UpdCustomer;