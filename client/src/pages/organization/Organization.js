import React, { useEffect, useRef } from "react";
import "devextreme/data/odata/store";
import DataGrid, {
  Column,
  Editing,
  Popup,
  Paging,
  Form,
  Pager,
  Toolbar,
  MasterDetail,
  Scrolling,
  LoadPanel,
} from "devextreme-react/data-grid";
import { Item, RequiredRule } from "devextreme-react/form";
import { useDispatch, useSelector } from "react-redux";
import DataSource from "devextreme/data/data_source";
import { Button } from "devextreme-react";
import {
  createOrganization,
  getOrganizationsByUser,
  removeOrganization,
  updateOrganization,
} from "../../redux/organizationReducer";
import "./organization.scss";
import ChecklistTemplate from "../../components/ChecklistTemplate";
import { getAllChecklists } from "../../redux/checklistReducer";
import { notifyError } from "../../functions/notifyError";

const Organization = () => {
  const dispatch = useDispatch();
  const selectedItemKeys = useRef([]);
  const organizations = useSelector(
    (state) => state.organization.organizations
  );
  const allowedPageSizes = [5, 10, 20, 50, 100];

  useEffect(() => {
    refreshDataGrid();
  }, []);

  const selectionChanged = (data) => {
    selectedItemKeys.current = data.selectedRowKeys;
  };

  const orgDataSource = new DataSource({
    store: {
      type: "array",
      data: JSON.parse(JSON.stringify(organizations)),
      key: "id",
    },
  });
  const onRowUpdating = async (e) => {
    const res = await dispatch(updateOrganization(e.data));
    notifyError(res);
    dispatch(getOrganizationsByUser());
  };
  const onRowInserting = async (e) => {
    const res = await dispatch(createOrganization(e.data));
    notifyError(res);
    dispatch(getOrganizationsByUser());
  };
  const onRowRemoving = async (e) => {
    const res = await dispatch(removeOrganization(e.data.id));
    notifyError(res);
    dispatch(getOrganizationsByUser());
  };
  const refreshDataGrid = () => {
    dispatch(getOrganizationsByUser());
    dispatch(getAllChecklists());
  };

  return (
    <React.Fragment>
      <h2 className={"content-block"}>Home</h2>
      <div className={"content-block"}>
        <div
          className={"dx-card responsive-paddings"}
          style={{ height: "100vh" }}
        >
          <DataGrid
            id="organization-container"
            className={"dx-card wide-card "}
            dataSource={orgDataSource}
            keyExpr="id"
            showColumnHeaders={false}
            showBorders={true}
            focusedRowEnabled={true}
            height={"100%"}
            columnAutoWidth={true}
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
            >
              <Popup
                title="Organization Info"
                showTitle={true}
                width={300}
                height={230}
              />
              <Form>
                <Item itemType="group" colCount={1} colSpan={2}>
                  <Item dataField="title">
                    <RequiredRule message="Title is required" />
                  </Item>
                </Item>
              </Form>
            </Editing>
            <Paging defaultPageSize={10} />
            <Pager
              showPageSizeSelector={true}
              showInfo={true}
              allowedPageSizes={allowedPageSizes}
            />
            <Column dataField="title" caption="Title" />
            <MasterDetail enabled={true} component={ChecklistTemplate} />
            <Toolbar>
              <Item location="before">
                <div className="organization">
                  <div className="title">Organizations</div>
                </div>
              </Item>
              <Item name="addRowButton" />
              <Item location="after">
                <Button icon="refresh" onClick={refreshDataGrid} />
              </Item>
            </Toolbar>
          </DataGrid>
        </div>
      </div>
    </React.Fragment>
  );
};
export default Organization;
