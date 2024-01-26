import {configureStore} from '@reduxjs/toolkit'
import {createEpicMiddleware} from "redux-observable";
import rootReducer from './reducer/root-reducer';
import { rootEpic } from './epic/root-epic';

const epicMiddleware = createEpicMiddleware();
const AppReduxStore = configureStore({
    reducer: rootReducer(),
    middleware: [epicMiddleware]
});
epicMiddleware.run(rootEpic);

export default AppReduxStore;