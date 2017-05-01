var mappingsObj = {"mappings":[{"COLUMN":0,"ROW":1,"VALUE":3,"expression":"ADD"},{"COLUMN":0,"ROW":1,"VALUE":84,"expression":"MULTIPLY"},{"COLUMN":0,"ROW":1,"VALUE":84,"expression":"SUBTRACT"},{"COLUMN":0,"VALUE":5,"expression":"ADDTOCOLUMN"},{"COLUMN":0,"expression":"AVERAGE"}]}
    
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

function AddtoColumn(dataObj, column, value) {
    for (var i = 1; i < dataObj.length - 1; i++) {
        dataObj = Addition(dataObj, i, column, value);
    }

    return dataObj;
}
    
    function Addition(dataObj, row, column, value){ 
    var testNumber = parseInt(dataObj[row][column]);
    testNumber += parseInt(value);

    dataObj[row][column] = testNumber;

    return dataObj;

}

function Multiply(dataObj, row, column, value){

    var testNumber = parseInt(dataObj[row][column]);
    testNumber = parseInt(value) * testNumber;

    dataObj[row][column] = testNumber;

    return dataObj;

}

function Subtract(dataObj, row, column, value){

    var testNumber = parseInt(dataObj[row][column]);
    testNumber = testNumber - parseInt(value);

    dataObj[row][column] = testNumber;

    return dataObj;

}


function Divide(dataObj, row, column, value){

    var testNumber = Number(dataObj[row][column]);
    testNumber = testNumber / Number(value);

    dataObj[row][column] = testNumber;

    return dataObj;

}

function Average(dataObj, column){

    var sumColumn = 0;

    for(var i = 1; i < dataObj.length - 1; i++){
        sumColumn += Number(dataObj[i][column]);
    }

    sumColumn = sumColumn / dataObj.length;
    
    dataObj[dataObj.length - 1][column] = sumColumn;
    return dataObj;

}