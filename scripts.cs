/**
 * This method adds a custom menu item to run the script
 */
function onOpen() {
  var ss = SpreadsheetApp.getActiveSpreadsheet();
  ss.addMenu('Convert to CSV',
             [{name: 'Convert to CSV', functionName: 'createCSV'}]);
}

function createCSV() {
  var ss = SpreadsheetApp.getActiveSpreadsheet(); //this is the sheet to be exported to csv
  
  var sheet = ss.getActiveSheet();
  var exportFileName = "newconvertedsheet.csv";
  var folder = DriveApp.getFolderById("0B-YC5XeGA1wwfmt5RkhleXNfWEo2MXd5aXJpNldwWlBPeU5KZlNZV0tZcjE5Zm5uSXl4eVU");
  fileName = sheet.getSheetName() + ".csv";
  // convert all available sheet data to csv format
  var csvFile = convertcsv(exportFileName, sheet);
  // create a file in the Docs List with the given name and the csv data
  var file = folder.createFile(exportFileName, csvFile);
}
function convertcsv(csvFileName, sheet) {
  // get available data range in the spreadsheet
  var activeRange = sheet.getDataRange();
  try {
    var data = activeRange.getValues();
    var csvFile = undefined;

    // loop through the data in the range and build a string with the csv data
    if (data.length > 1) {
      var csv = "";
      for (var row = 0; row < data.length; row++) {
        for (var col = 0; col < data[row].length; col++) {
          if (data[row][col].toString().indexOf(",") != -1) {
            data[row][col] = "\"" + data[row][col] + "\"";
          }
        }
        if (row < data.length-1) {
          csv += data[row].join(",") + "\r\n";
        }
        else {
          csv += data[row];
        }
      }
      csvFile = csv;
    }
    return csvFile;
  }
  catch(err) {
    Logger.log(err);
  }
}