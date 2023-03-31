import React, { useCallback, useEffect, useRef, useState } from "react";
import "devextreme/data/odata/store";
import DataGrid, {
  Column,
  Editing,
  Popup,
  Paging,
  Form,
  Pager,
  Toolbar,
  Lookup,
  LoadPanel,
} from "devextreme-react/data-grid";
import { Item, RequiredRule } from "devextreme-react/form";
import { useDispatch, useSelector } from "react-redux";
import DataSource from "devextreme/data/data_source";
import { Button } from "devextreme-react";
import {
  createSubscription,
  getAllSubscriptions,
  removeSubscription,
  updateSubscription,
} from "../../redux/subscriptionReducer";
import { request } from "../../server/api";
import { notifyError } from "../../functions/notifyError";

const SubscriptionTemplate = (props) => {
  const dispatch = useDispatch();
  const [orgData, setOrgData] = useState([]);
  const [showOrg, setShowOrg] = useState(true);
  const { id } = props.data.data;
  const selectedItemKeys = useRef([]);
  const subscriptions = useSelector(
    (state) => state.subscription.subscriptions
  );
  const allowedPageSizes = [5, 10, 20, 50, 100];

  useEffect(() => {
    refreshDataGrid();
  }, [id]);

  const dataSource = useCallback(
    new DataSource({
      store: {
        data: JSON.parse(JSON.stringify(subscriptions)),
        key: "id",
        type: "array",
      },
      filter: ["userId", "=", props.data.key],
    }),
    [subscriptions]
  );
  const getOrgData = async (id) => {
    const response = await request({
      requestUrl: `Subscription/getOrgsByUserId/${id}`,
      apiType: "get",
    });
    setOrgData(response.data);
  };
  const selectionChanged = (data) => {
    selectedItemKeys.current = data.selectedRowKeys;
  };
  const onEditingStart = (e) => {
    setShowOrg(false);
  };
  const onInitNewRow = (e) => {
    e.data.canList = false;
    e.data.canAdd = false;
    e.data.canDelete = false;
    e.data.canEdit = false;
    getOrgData(id);
  };
  const onRowUpdating = (e) => {
    dispatch(updateSubscription({ ...e.oldData, ...e.newData }));
    setShowOrg(true);
  };
  const onRowInserting = async (e) => {
    let res = await dispatch(createSubscription({ ...e.data, userId: id }));
    notifyError(res);
    refreshDataGrid();
  };
  const onRowRemoving = async (e) => {
    await dispatch(removeSubscription(e.data.id));
    refreshDataGrid();
  };
  const onEditCancelling = async (e) => {
    setShowOrg(true);
  };
  const refreshDataGrid = () => {
    dispatch(getAllSubscriptions());
    getOrgData(id);
  };

  return (
    <React.Fragment>
      <DataGrid
        id="subscription-container"
        className={"dx-card wide-card"}
        dataSource={dataSource}
        showColumnHeaders={true}
        showBorders={true}
        height={"100%"}
        columnAutoWidth={true}
        columnHidingEnabled={true}
        onEditingStart={onEditingStart}
        onEditCanceling={onEditCancelling}
        onInitNewRow={onInitNewRow}
        onSelectionChanged={selectionChanged}
        onRowInserting={onRowInserting}
        onRowUpdating={onRowUpdating}
        onRowRemoving={onRowRemoving}
      >
        <LoadPanel enabled={true} />
        <Editing
          mode="popup"
          allowUpdating={true}
          allowAdding={orgData.length > 0}
          allowDeleting={true}
          labelLocation="left"
        >
          <Popup
            title="Subscription Info"
            showTitle={true}
            width="auto"
            height="auto"
          />
          <Form
            labelLocation="left"
            labelMode="outside"
            alignItemLabels={true}
            alignItemLabelsInAllGroups={true}
          >
            <Item itemType="group" colCount={2} colSpan={2}>
              <Item
                dataField="organizationId"
                caption="Organization"
                colSpan={2}
                visible={showOrg}
              >
                <RequiredRule message="Title is required" />
              </Item>
              <Item dataField="canList" />
              <Item dataField="canAdd" />
              <Item dataField="canDelete" />
              <Item dataField="canEdit" />
            </Item>
          </Form>
        </Editing>
        <Paging defaultPageSize={10} />
        <Pager
          showPageSizeSelector={true}
          showInfo={true}
          allowedPageSizes={allowedPageSizes}
        />
        <Column
          dataField="organizationId"
          caption="Organization"
          visible={false}
        >
          <Lookup dataSource={orgData} displayExpr="title" valueExpr="id" />
        </Column>
        <Column dataField="title" caption="Organization" />
        <Column dataField="canList" caption="List" dataType="boolean" />
        <Column dataField="canAdd" caption="Add" dataType="boolean" />
        <Column dataField="canDelete" caption="Delete" dataType="boolean" />
        <Column dataField="canEdit" caption="Edit" dataType="boolean" />
        <Toolbar>
          <Item location="before">
            <div className="organization">
              <div className="title">Subscriptions</div>
            </div>
          </Item>
          <Item name="addRowButton" />
          <Item location="after">
            <Button icon="refresh" onClick={refreshDataGrid} />
          </Item>
        </Toolbar>
      </DataGrid>
    </React.Fragment>
  );
};

export default SubscriptionTemplate;
