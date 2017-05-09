/* Used for downloading a file from API Gateway. Should be integrated into a webpage in the future */

var express = require('express');
var app = express();
var request = require('request');
var fs = require('fs');

// URL to API Gateway goes here!
var url = "https://zrn9yvy6a8.execute-api.ap-southeast-2.amazonaws.com/Test";

app.set('port', (process.env.PORT || 5000));

app.get('/', function(request, response) {
  
});

var myJSONObject = JSON.parse(fs.readFileSync('Integration.json', 'utf8'));

  request({
      url: url,
      method: "POST",
      json: true,   // <--Very important!!!
      body: myJSONObject
  }, function (error, response, body){
      fs.writeFile("functions.js", body, function(err) {
        if(err) {
            return console.log(err);
        }

        console.log("The file was saved!");
    }); 
  });

app.listen(app.get('port'), function() {
  console.log('Node app is running on port', app.get('port'));
});

