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

    dataObj[dataObj.length][column] = sumColumn;
    return dataObj;

}