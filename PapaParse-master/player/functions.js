var COLUMN = '3';var ROW = '3';var VALUE = '3';var expression = 'ADD';
    
    function run(dataObj){
    switch(expression) {
    case "ADD":
        return Addition(dataObj, ROW, COLUMN,VALUE);
        break;
    case "SUBTRACT":
        return Subtract(dataObj, ROW, COLUMN,VALUE);
        break;
    default:
        
    }
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

    return sumColumn;

}