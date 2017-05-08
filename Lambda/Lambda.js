'use strict';

var AddToColumn = `function AddtoColumn(dataObj, column, value) {
    for (var i = 1; i < dataObj.length - 1; i++) {
        dataObj = Addition(dataObj, i, column, value);
    }

    return dataObj;
}`;

var Add = `function Addition(dataObj, row, column, value){ 
    var testNumber = parseInt(dataObj[row][column]);
    testNumber += parseInt(value);

    dataObj[row][column] = testNumber;

    return dataObj;

}`;

var Subtraction = `function Subtract(dataObj, row, column, value){

    var testNumber = parseInt(dataObj[row][column]);
    testNumber = testNumber - parseInt(value);

    dataObj[row][column] = testNumber;

    return dataObj;

}
`;
var Multiply = `function Multiply(dataObj, row, column, value){

    var testNumber = parseInt(dataObj[row][column]);
    testNumber = parseInt(value) * testNumber;

    dataObj[row][column] = testNumber;

    return dataObj;

}`;
var Divide = `function Divide(dataObj, row, column, value){

    var testNumber = Number(dataObj[row][column]);
    testNumber = testNumber / Number(value);

    dataObj[row][column] = testNumber;

    return dataObj;

}`;
var Average = `
function Average(dataObj, column){

    var sumColumn = 0;

    for(var i = 1; i < dataObj.length - 1; i++){
        sumColumn += Number(dataObj[i][column]);
    }

    sumColumn = sumColumn / dataObj.length;
    
    dataObj[dataObj.length - 1][column] = sumColumn;
    return dataObj;

}`;

exports.handler = (event, context, callback) => {
    
    var application = ``;
    
    application += 'var mappingsObj = '+JSON.stringify(event)
    application += `
    function run(dataObj) {
        for (var key in mappingsObj.mappings) {
            switch (mappingsObj.mappings[key].expression) {
                case "ADD":
                    dataObj = Addition(dataObj, mappingsObj.mappings[key].ROW, mappingsObj.mappings[key].COLUMN, mappingsObj.mappings[key].VALUE);
                    break;
                case "SUBTRACT":
                    dataObj = Subtract(dataObj, mappingsObj.mappings[key].ROW, mappingsObj.mappings[key].COLUMN, mappingsObj.mappings[key].VALUE);
                    break;
                case "MULTIPLY":
                    dataObj = Multiply(dataObj, mappingsObj.mappings[key].ROW, mappingsObj.mappings[key].COLUMN, mappingsObj.mappings[key].VALUE);
                    break;
                case "DIVIDE":
                    dataObj = Divide(dataObj, mappingsObj.mappings[key].ROW, mappingsObj.mappings[key].COLUMN, mappingsObj.mappings[key].VALUE);
                    break;
                case "AVERAGE":
                    dataObj = Average(dataObj, mappingsObj.mappings[key].COLUMN);
                    break;
                case "ADDTOCOLUMN":
                    dataObj = AddtoColumn(dataObj, mappingsObj.mappings[key].COLUMN, mappingsObj.mappings[key].VALUE);
                    break;
                default:
            }
        }
        return dataObj;
    }
`;
    
    for (var key in event.mappings) {
        switch (event.mappings[key].expression) {
                case "ADD":
                    application += Add;
                    break;
                case "SUBTRACT":
                    application += Subtraction;
                    break;
                case "MULTIPLY":
                    application += Multiply;
                    break;
                case "DIVIDE":
                    application += Divide;
                    break;
                case "AVERAGE":
                    application += Average;
                    break;
                case "ADDTOCOLUMN":
                    application += AddToColumn;
                    break;
                default:
            }
    }
    
    

    
    callback(null,application);  // Echo back the first key value
};
