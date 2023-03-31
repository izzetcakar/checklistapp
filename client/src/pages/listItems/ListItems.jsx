import React, { useEffect, useRef, useState } from "react";
import "devextreme/data/odata/store";
import { Workbook } from "exceljs";
import { saveAs } from "file-saver-es";
import { exportDataGrid } from "devextreme/excel_exporter";
import DataGrid, {
  Column,
  Editing,
  Popup,
  Paging,
  Lookup,
  Form,
  Pager,
  FilterRow,
  Export,
  Selection,
  Toolbar,
  ColumnChooser,
  ColumnFixing,
  LoadPanel,
} from "devextreme-react/data-grid";
import { Item, RequiredRule, RangeRule } from "devextreme-react/form";
import { useDispatch, useSelector } from "react-redux";
import {
  createListItem,
  getListItemsByChkId,
  removeListItem,
  removeListItemByIds,
  updateListItem,
} from "../../redux/listItemReducer";
import DataSource from "devextreme/data/data_source";
import { Button } from "devextreme-react";
import { notifyError } from "../../functions/notifyError";
import { getAllBaseOptions } from "../../redux/baseOptionReducer";
import { exportExcel } from "../../components/Import/export-excel";
import { ImportExcel } from "../../components/Import/import-excel";

const ListItems = ({ Id }) => {
  const dispatch = useDispatch();
  const effect = useRef(false);
  const selectedItemKeys = useRef([]);
  const [test, setTest] = useState();
  const listItems = useSelector((state) => state.listItem.listItems);
  const categories = useSelector((state) => state.baseOption.categories);
  const segments = useSelector((state) => state.baseOption.segments);
  const controlLists = useSelector((state) => state.baseOption.controlLists);
  const consepts = useSelector((state) => state.baseOption.consepts);
  const contents = useSelector((state) => state.baseOption.contents);

  const notesEditorOptions = { height: "auto" };
  const allowedPageSizes = [5, 10, 20, 50, 100];

  useEffect(() => {
    if (effect.current === false) {
      return () => {
        effect.current = true;
      };
    } else {
      refreshDataGrid();
    }
  }, [Id]);
  const columns = [
    { label: "Konsept", value: "conseptId" },
    { label: "Kategori", value: "categoryId" },
    { label: "Alan", value: "segmentId" },
    { label: "Kontrol Listesi", value: "controlListId" },
    { label: "İçerik", value: "contentId" },
    { label: "Risk Önem Düzeyi", value: "risk" },
    { label: "Standard", value: "standard" },
    { label: "Uygunluk", value: "relevance" },
    { label: "Departman", value: "department" },
    { label: "Not", value: "note" },
  ];
  const refreshDataGrid = () => {
    dispatch(getListItemsByChkId(Id));
    dispatch(getAllBaseOptions());
  };
  const risks = [
    { value: "A" },
    { value: "B" },
    { value: "C" },
    { value: "D" },
    { value: "E" },
    { value: "F" },
  ];
  const listDataSource = new DataSource({
    store: {
      type: "array",
      data: JSON.parse(JSON.stringify(listItems)),
      key: "id",
    },
  });
  const onExporting = (e) => {
    const workbook = new Workbook();
    const worksheet = workbook.addWorksheet("Main sheet");

    exportDataGrid({
      component: e.component,
      worksheet,
      autoFilterEnabled: true,
    }).then(() => {
      workbook.xlsx.writeBuffer().then((buffer) => {
        saveAs(
          new Blob([buffer], { type: "application/octet-stream" }),
          "LibertyChecklist.xlsx"
        );
      });
    });
    e.cancel = true;
  };
  const onRowUpdating = async (e) => {
    const res = await dispatch(updateListItem({ ...e.oldData, ...e.newData }));
    notifyError(res);
    dispatch(getListItemsByChkId(Id));
  };
  const onRowInserting = async (e) => {
    const res = await dispatch(createListItem({ ...e.data, checklistId: Id }));
    notifyError(res);
    dispatch(getListItemsByChkId(Id));
  };
  const onRowRemoving = async (e) => {
    const res = await dispatch(removeListItem(e.data.id));
    notifyError(res);
    dispatch(getListItemsByChkId(Id));
  };
  const deleteRecords = async () => {
    let inputIds = [];
    selectedItemKeys.current.forEach((key) => {
      inputIds.push(key);
    });
    const res = await dispatch(removeListItemByIds(inputIds));
    notifyError(res);
    selectedItemKeys.current = [];
    dispatch(getListItemsByChkId(Id));
  };
  const selectionChanged = (data) => {
    selectedItemKeys.current = data.selectedRowKeys;
  };

  return (
    <React.Fragment>
      {/* <div style={{ fontSize: "2em", marginBottom: "0.3em" }}>
        {checklist?.title}
      </div> */}
      <DataGrid
        className={"dx-card wide-card"}
        dataSource={listDataSource}
        height={"100%"}
        showBorders={true}
        focusedRowEnabled={true}
        defaultFocusedRowIndex={0}
        columnAutoWidth={false}
        columnHidingEnabled={false}
        selectedRowKeys={selectedItemKeys.current}
        onRowInserting={onRowInserting}
        onRowUpdating={onRowUpdating}
        onRowRemoving={onRowRemoving}
        onExporting={onExporting}
        onSelectionChanged={selectionChanged}
        allowColumnReordering={true}
        allowColumnResizing={true}
      >
        <LoadPanel enabled={true} />
        <Editing
          mode="popup"
          allowUpdating={true}
          allowAdding={true}
          allowDeleting={true}
        >
          <Popup
            title="List Item Info"
            showTitle={true}
            width={700}
            height={525}
          />
          <Form>
            <Item itemType="group" colCount={2} colSpan={2}>
              <Item dataField="conseptId" />
              <Item dataField="categoryId" />
              <Item dataField="segmentId" />
              <Item dataField="controlListId" />
              <Item dataField="contentId" />
              <Item dataField="risk" />
              <Item dataField="standard" />
              <Item dataField="relevance" />
              <Item dataField="department" />
              <Item
                dataField="note"
                itemType="dxTextArea"
                colSpan={2}
                editorOptions={notesEditorOptions}
              />
            </Item>
          </Form>
        </Editing>
        <Paging defaultPageSize={10} />
        <Pager
          visible={true}
          showPageSizeSelector={true}
          showInfo={true}
          allowedPageSizes={allowedPageSizes}
        />
        <FilterRow visible={true} />
        {/* <Column dataField={"id"} width={90} hidingPriority={2} /> */}
        <Column dataField="conseptId" caption="Consept">
          <RequiredRule message="Consept List is required" />
          <Lookup dataSource={consepts} displayExpr="title" valueExpr="id" />
        </Column>
        <Column dataField="categoryId" caption="Category">
          <RequiredRule message="Category is required" />
          <Lookup dataSource={categories} displayExpr="title" valueExpr="id" />
        </Column>
        <Column dataField="segmentId" caption="Segment">
          <RequiredRule message="Segment is required" />
          <Lookup dataSource={segments} displayExpr="title" valueExpr="id" />
        </Column>
        <Column dataField="controlListId" caption="Control List">
          <RequiredRule message="Control List is required" />
          <Lookup
            dataSource={controlLists}
            displayExpr="title"
            valueExpr="id"
          />
        </Column>
        <Column dataField="contentId" caption="Content">
          <RequiredRule message="Content is required" />
          <Lookup dataSource={contents} displayExpr="title" valueExpr="id" />
        </Column>
        <Column dataField="standard" caption="Standard" alignment={"center"}>
          <RequiredRule message="Standard is required" />
          <RangeRule
            message="Standard should be in between 0 and 10"
            min={0}
            max={10}
          />
        </Column>
        <Column dataField="risk" caption="Risk" alignment={"center"}>
          <RequiredRule message="Risk is required" />
          <Lookup dataSource={risks} valueExpr="value" displayExpr="value" />
        </Column>
        <Column
          dataField="itemScore"
          caption="Item Score"
          alignment={"center"}
        />
        <Column dataField="relevance" caption="Relevance" alignment={"center"}>
          <RequiredRule message="Relevance is required" />
          <RangeRule
            message="Relevance  should be in between 0 and 10"
            min={0}
            max={10}
          />
        </Column>
        <Column
          dataField="result"
          caption="Result"
          alignment={"center"}
          format="percent"
        />
        <Column dataField="department" caption="Departmant">
          <RequiredRule message="Department is required" />
        </Column>
        <Column dataField="note" caption="Note" />
        <Selection mode="multiple" />
        <Toolbar>
          <Item location="before">
            <Button onClick={deleteRecords} icon="trash" text="Delete Items" />
          </Item>
          <Item location="before">
            <Button
              type={"success"}
              stylingMode="outlined"
              name="shape-budget-download"
              icon="export"
              text="Download"
              onClick={(e) =>
                exportExcel({
                  sheetName: "Checklist",
                  columns: columns,
                  fileName: "Checklist",
                })
              }
            />
          </Item>
          <Item name="addRowButton" showText="always" />
          <Item name="columnChooserButton" />
          <Item location="after">
            <Button icon="refresh" onClick={refreshDataGrid} />
          </Item>
          <Item name="exportButton" />
        </Toolbar>
        <Export enabled={true} allowExportSelectedData={true} />
        <ColumnChooser enabled={true} />
        <ColumnFixing enabled={true} />
      </DataGrid>
      <ImportExcel textVisible={true} setResult={setTest} columns={columns} />
    </React.Fragment>
  );
};
export default ListItems;
