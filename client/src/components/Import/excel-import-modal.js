import React, { useState, useEffect, useRef } from "react";
import { Popup } from "devextreme-react/popup";
import { useGuide } from "../../contexts/hooks";

export const ExcelImportModal = ({ open, onClose, onSelect }) => {
  const modal = useRef();
  const [editorMode, setEditorMode] = useState(false);
  const [data, setData] = useState([]);
  const guide = useGuide();
  const [contentHeight, setContentHeight] = useState(0);

  useEffect(() => {
    if (open) {
      guide.getCariList().then((x) => {
        setData(x.data);
      });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [open]);

  document.getElementsByClassName("dx-popup-wrapper");

  return (
    <Popup
      visible={open}
      ref={modal}
      onHiding={onClose}
      dragEnabled={true}
      closeOnOutsideClick={false}
      showTitle={true}
      resizeEnabled={true}
      title="Cari Rehberi"
      onShown={(e) =>
        setContentHeight(document.getElementById("modal-content").clientHeight)
      }
      toolbarItems={[
        {
          location: "before",
          widget: "dxButton",
          options: {
            stylingMode: "text",
            icon: !editorMode ? "edit" : "rowfield",
            text: !editorMode ? "Yeni Cari" : "Liste",
            onClick: (e) => setEditorMode(!editorMode),
          },
        },
      ]}
      showCloseButton={true}
    >
      <div id={"modal-content"} style={{ height: "100%" }}>
        {editorMode === false ? (
          <List data={data} height={contentHeight} onSelect={onSelect} />
        ) : (
          editor()
        )}
      </div>
    </Popup>
  );
};
