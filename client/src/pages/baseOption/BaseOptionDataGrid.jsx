import React, { useRef } from "react";
import "devextreme/data/odata/store";
import DataGrid, {
  Column,
  Editing,
  Popup,
  Paging,
  Form,
  Pager,
  Toolbar,
  LoadPanel,
} from "devextreme-react/data-grid";
import { Item, RequiredRule } from "devextreme-react/form";
import { useDispatch } from "react-redux";
import DataSource from "devextreme/data/data_source";
import {
  createBaseOption,
  getAllBaseOptions,
  removeBaseOption,
  updateBaseOption,
} from "../../redux/baseOptionReducer";
import { notifyError } from "../../functions/notifyError";
import { Button } from "devextreme-react";

const BaseOptionDataGrid = ({ optionData, optionType }) => {
  const dispatch = useDispatch();
  const selectedItemKeys = useRef([]);
  const allowedPageSizes = [5, 10, 20, 50, 100];
  const dataGridId = optionType + "-category-datagrid";
  const title = optionType + " Info";

  const optionDataSource = new DataSource({
    store: {
      type: "array",
      data: JSON.parse(JSON.stringify(optionData)),
      key: "id",
    },
  });

  const selectionChanged = (data) => {
    selectedItemKeys.current = data.selectedRowKeys;
  };
  const onRowUpdating = async (e) => {
    let updateObj = { ...e.oldData, ...e.newData };
    await dispatch(updateBaseOption({ ...updateObj, optionType: optionType }));
    refreshDataGrid();
  };
  const onRowInserting = async (e) => {
    const res = await dispatch(
      createBaseOption({ ...e.data, optionType: optionType })
    );
    notifyError(res);
    refreshDataGrid();
  };
  const onRowRemoving = async (e) => {
    await dispatch(removeBaseOption({ id: e.data.id, optionType: optionType }));
    refreshDataGrid();
  };
  const refreshDataGrid = () => {
    dispatch(getAllBaseOptions());
  };

  return (
    <DataGrid
      id={dataGridId}
      className={"dx-card wide-card"}
      dataSource={optionDataSource}
      showColumnHeaders={false}
      showBorders={true}
      height={"inherit"}
      columnHidingEnabled={true}
      onSelectionChanged={selectionChanged}
      onRowInserting={onRowInserting}
      onRowUpdating={onRowUpdating}
      onRowRemoving={onRowRemoving}
    >
      <LoadPanel enabled={true} />
      <Editing
        mode="popup"
        allowUpdating={true}
        allowAdding={true}
        allowDeleting={true}
        labelLocation="left"
      >
        <Popup
          title={title}
          showTitle={true}
          maxWidth="50%"
          width="inherit"
          height="auto"
        />
        <Form
          labelLocation="left"
          labelMode="outside"
          alignItemLabels={true}
          alignItemLabelsInAllGroups={true}
        >
          <Item itemType="group" colCount={2} colSpan={2}>
            <Item dataField="title" colSpan={2}>
              <RequiredRule message="Title is required" />
            </Item>
          </Item>
        </Form>
      </Editing>
      <Paging defaultPageSize={5} />
      <Pager
        visible={true}
        showPageSizeSelector={true}
        showInfo={true}
        allowedPageSizes={allowedPageSizes}
      />
      <Column dataField="title" />
      <Toolbar>
        <Item location="before">
          <div className="organization">
            <div className="title">{optionType}</div>
          </div>
        </Item>
        <Item name="addRowButton" />
        <Item location="after">
          <Button icon="refresh" onClick={refreshDataGrid} />
        </Item>
      </Toolbar>
    </DataGrid>
  );
};

export default BaseOptionDataGrid;
