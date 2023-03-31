import { Button, Popup } from "devextreme-react";
import { Position, ToolbarItem } from "devextreme-react/popup";
import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { notifyError } from "../../functions/notifyError";
import {
  getAllPermissions,
  replyPermission,
} from "../../redux/permissionReducer";

const PermissionEditCell = (cellData) => {
  let data = cellData.data.data;
  const dispatch = useDispatch();
  const [visible, setVisible] = useState(false);
  const [approval, setApproval] = useState();
  const [sendable, setSendable] = useState(false);

  const Apply = () => {
    setVisible(true);
    setApproval(true);
    setSendable(false);
  };
  const Reject = () => {
    setVisible(true);
    setApproval(false);
    setSendable(false);
  };
  const hidePopup = () => {
    setVisible(false);
  };
  const checkBoxHandler = (e) => {
    setSendable((prev) => e.value);
  };
  const checkBoxOptions = {
    text: "Check",
    value: sendable,
    onValueChanged: checkBoxHandler,
  };
  const sendRequest = async (approval) => {
    if (approval) {
      let res = await dispatch(replyPermission({ ...data, status: 1 }));
      notifyError(res);
      dispatch(getAllPermissions());
    } else {
      let res = await dispatch(replyPermission({ ...data, status: 0 }));
      notifyError(res);
      dispatch(getAllPermissions());
    }
    hidePopup();
  };
  const applyButtonOptions = {
    icon: "email",
    text: "Send",
    onClick: () => sendRequest(approval),
  };
  return (
    <>
      {data.status === "Waiting" ? (
        <div
          style={{
            display: "flex",
            justifyContent: "space-around",
          }}
        >
          <Button icon="check" onClick={Apply} />
          <Button icon="remove" onClick={Reject} />
          <Popup
            visible={visible}
            onHiding={hidePopup}
            dragEnabled={true}
            hideOnOutsideClick={true}
            showCloseButton={true}
            showTitle={false}
            title="Approval"
            width={200}
            height={60}
            container=".dx-viewport"
          >
            <Position at="center" my="center" collision="fit" />

            <ToolbarItem
              widget="dxCheckBox"
              toolbar="top"
              location="before"
              options={checkBoxOptions}
            />
            <ToolbarItem
              disabled={!sendable}
              widget="dxButton"
              toolbar="top"
              location="after"
              options={applyButtonOptions}
            />
          </Popup>
        </div>
      ) : null}
    </>
  );
};

export default PermissionEditCell;
