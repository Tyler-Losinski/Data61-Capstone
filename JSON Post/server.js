var express = require('express');
var app = express();
var request = require('request');

app.set('port', (process.env.PORT || 5000));

app.get('/', function(request, response) {
  
});

var myJSONObject = { json: { key: 'value' } };
  request({
      url: "https://ua1c36to9i.execute-api.ap-southeast-2.amazonaws.com/beta",
      method: "POST",
      json: true,   // <--Very important!!!
      body: myJSONObject
  }, function (error, response, body){
      console.log(response);
  });

app.listen(app.get('port'), function() {
  console.log('Node app is running on port', app.get('port'));
});

