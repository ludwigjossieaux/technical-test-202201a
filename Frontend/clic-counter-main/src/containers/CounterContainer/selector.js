import { createSelector } from 'reselect';
import { initialState } from './reducer';

const selectCounterContainerDomain = state => state.counterContainer || initialState;
const makeSelectCounterContainerCounter = () => createSelector(selectCounterContainerDomain, substate => substate.counter);

export {
    makeSelectCounterContainerCounter,
};
