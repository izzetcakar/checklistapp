import "./themes/generated/theme.additional.css";
import "./dx-styles.scss";
import React from "react";
import { HashRouter as Router } from "react-router-dom";
import LoadPanel from "devextreme-react/load-panel";
import { NavigationProvider } from "./contexts/navigation";
import { AuthProvider, useAuth } from "./contexts/auth";
import { useScreenSizeClass } from "./utils/media-query";
import Content from "./Content";
import UnauthenticatedContent from "./UnauthenticatedContent";
import { useSelector } from "react-redux";

function App() {
  const userRedux = useSelector((state) => state.user.user);
  const { loading } = useAuth();

  if (loading) {
    return <LoadPanel visible={true} />;
  }

  if (userRedux) {
    return <Content />;
  }

  return <UnauthenticatedContent />;
}

export default function Root() {
  const screenSizeClass = useScreenSizeClass();

  return (
    <Router>
      <AuthProvider>
        <NavigationProvider>
          <div className={`app ${screenSizeClass}`}>
            <App />
          </div>
        </NavigationProvider>
      </AuthProvider>
    </Router>
  );
}
