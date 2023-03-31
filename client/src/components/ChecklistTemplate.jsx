import React, { useCallback, useEffect, useState } from "react";
import DataGrid, {
  Column,
  Editing,
  Popup,
  Form,
  Export,
  Toolbar,
  Item,
} from "devextreme-react/data-grid";
import { Popup as NormalPopup } from "devextreme-react/popup";
import DataSource from "devextreme/data/data_source";
import { useDispatch, useSelector } from "react-redux";
import {
  createChecklist,
  getAllChecklists,
  removeChecklist,
  updateChecklist,
} from "../redux/checklistReducer";
import { RequiredRule } from "devextreme-react/form";
import { notifyError } from "../functions/notifyError";
import { Button, ScrollView } from "devextreme-react";
import ListItems from "../pages/listItems/ListItems";

const ChecklistTemplate = (props) => {
  const dispatch = useDispatch();
  const checklists = useSelector((state) => state.checklist.checklists);
  const [visible, setVisible] = useState(false);
  const [checklistId, setChecklistId] = useState();

  useEffect(() => {
    dispatch(getAllChecklists());
  }, [visible]);

  const dataSource = useCallback(
    new DataSource({
      store: {
        data: JSON.parse(JSON.stringify(checklists)),
        key: "id",
        type: "array",
      },
      filter: ["organizationId", "=", props.data.key],
    }),
    [checklists]
  );
  const { title } = props.data.data;

  const onRowUpdating = async (e) => {
    const res = await dispatch(updateChecklist({ ...e.oldData, ...e.newData }));
    notifyError(res);
    dispatch(getAllChecklists());
  };
  const onRowInserting = async (e) => {
    const res = await dispatch(
      createChecklist({ ...e.data, organizationId: props.data.data.id })
    );
    notifyError(res);
    dispatch(getAllChecklists());
  };
  const onRowRemoving = async (e) => {
    const res = await dispatch(removeChecklist(e.data.id));
    notifyError(res);
    dispatch(getAllChecklists());
  };
  const directToList = ({ data }) => {
    const test = async () => {
      await setChecklistId(data.row.data.id);
      setVisible(true);
    };
    return (
      <div style={{ display: "flex", justifyContent: "center" }}>
        <Button text="Open" onClick={() => test()} />
      </div>
    );
  };
  const hidePopup = () => {
    setVisible(false);
  };
  return (
    <React.Fragment>
      <DataGrid
        dataSource={dataSource}
        showBorders={true}
        columnAutoWidth={false}
        noDataText="No Checklist"
        onRowInserting={onRowInserting}
        onRowUpdating={onRowUpdating}
        onRowRemoving={onRowRemoving}
        showRowLines={true}
      >
        <Editing
          mode="popup"
          allowUpdating={true}
          allowAdding={true}
          allowDeleting={true}
        >
          <Popup
            title="Checklist Info"
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
        <Column dataField="title" />
        <Column dataField="createdDate" dataType="date" />
        <Column dataField="updatedDate" dataType="date" />
        <Column cellComponent={directToList} visible={true} />
        <Toolbar>
          <Item location="before">
            <div className="organization">
              <div className="title">{title}'s Checklists</div>
            </div>
          </Item>
          <Item name="addRowButton" />
          <Item name="exportButton" />
        </Toolbar>
        <Export enabled={true} allowExportSelectedData={true} />
      </DataGrid>
      <NormalPopup
        visible={visible}
        onHiding={hidePopup}
        showCloseButton={true}
        showTitle={false}
        container=".dx-viewport"
        hideOnOutsideClick={true}
      >
        <ScrollView width="100%" height="100%">
          <ListItems Id={checklistId} />
        </ScrollView>
      </NormalPopup>
    </React.Fragment>
  );
};

export default ChecklistTemplate;
