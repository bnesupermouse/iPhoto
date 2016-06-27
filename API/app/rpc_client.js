
module.exports = function()
{
var amqp = require('amqplib/callback_api');

amqp.connect('amqp://localhost', function(err, conn) {
  conn.createChannel(function(err, ch) {
    ch.assertQueue('', {exclusive: true}, function(err, q) {
      var corr = generateUuid();
      var num = 58;

      console.log(' [x] Requesting fib(%d)', num);

      ch.consume(q.queue, function(msg) {
        if (msg.properties.correlationId == corr) {
          console.log(' [.] Got %s', msg.content.toString());
          setTimeout(function() { conn.close(); process.exit(0) }, 500);
        }
      }, {noAck: true});

var Customer = require('./models/customer.js');
      var newCtm = new Customer();
      newCtm.CustomerName = "nodetest";
      newCtm.Email = "nodejs5@node.com";
      newCtm.Password = "nodejs";
      newCtm.Age = 18;
      var UpdCustomer = require('./models/UpdCustomer.js');
      var updCtm = new UpdCustomer();
      updCtm.NewCustomer = newCtm;

      var msgStr = JSON.stringify(updCtm);
      var Request = require('./models/Request.js');
      var req = new Request();
      req.TxnId = 1;
      req.Msg = msgStr;
      var message = JSON.stringify(req);

      console.log(' [x] Requesting fib(%d)', num);
      ch.sendToQueue('rpc_queue',
        new Buffer(message),
        { correlationId: corr, replyTo: q.queue });
    });
  });
});
};

function generateUuid() {
  return Math.random().toString() +
         Math.random().toString() +
         Math.random().toString();
}