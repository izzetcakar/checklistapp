import { Routes, Route, Navigate } from "react-router-dom";
import { SingleCard } from "./layouts";
import { LoginForm, CreateAccountForm } from "./components";

export default function UnauthenticatedContent() {
  return (
    <Routes>
      <Route
        path="/login"
        element={
          <SingleCard title="Sign In">
            <LoginForm />
          </SingleCard>
        }
      />
      <Route
        path="/create-account"
        element={
          <SingleCard title="Sign Up">
            <CreateAccountForm />
          </SingleCard>
        }
      />
      <Route path="*" element={<Navigate to={"/login"} />}></Route>
    </Routes>
  );
}
