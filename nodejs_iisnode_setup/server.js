var express = require('express');

var app = express();

app.get('/', function (req, res) {
	res.send('Express is working on IISnode!');
});

app.listen(process.env.PORT);
