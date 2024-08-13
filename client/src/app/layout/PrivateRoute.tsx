import { ReactElement } from "react";
import { Navigate } from "react-router-dom";
import { useAppSelector } from "../store/configureStore";

interface PrivateRouteProps {
    element: ReactElement;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ element }) => {
    const { user } = useAppSelector(state => state.account);

    return user ? element : <Navigate to="/login" replace />;
};

export default PrivateRoute;