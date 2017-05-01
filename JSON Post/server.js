var express = require('express');
var app = express();
var request = require('request');
var fs = require('fs');

app.set('port', (process.env.PORT || 5000));

app.get('/', function(request, response) {
  
});

var myJSONObject = JSON.parse(fs.readFileSync('Integration.json', 'utf8'));

  request({
      url: "https://3gzpbgxbk1.execute-api.us-east-1.amazonaws.com/prod/Capstone",
      method: "POST",
      json: true,   // <--Very important!!!
      body: myJSONObject
  }, function (error, response, body){
      fs.writeFile("/Users/dotsc_67/Desktop/Repositories/Git/Data61-Capstone/PapaParse-master/player/functions.js", body, function(err) {
        if(err) {
            return console.log(err);
        }

        console.log("The file was saved!");
    }); 
  });

app.listen(app.get('port'), function() {
  console.log('Node app is running on port', app.get('port'));
});

