var mk = require('safe-mkdir');
var gc = require('glob-copy');

mk.mkdir('./typings/modules');
gc('./node_modules/aurelia-*/dist/system/*.d.ts', './typings/modules', function (err, files) {

    console.log('Copied ' + files.length + ' files to ./typings/modules');
});