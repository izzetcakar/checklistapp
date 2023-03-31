import React from "react";
import { writeFile, utils } from "xlsx";

export const exportExcel = ({ sheetName, data, columns, fileName }) => {
  if (columns) {
    data = columns.map((x) => {
      var row = {};
      columns.map((c) => {
        row = { ...row, [c.label]: x[c.value] };
      });
      return row;
    });
  }
  const worksheet = utils.json_to_sheet(data);
  const workbook = utils.book_new();
  utils.book_append_sheet(
    workbook,
    worksheet,
    sheetName ? sheetName : "sheet1"
  );
  writeFile(workbook, (fileName ? fileName : "export-data") + ".xlsx");
};
