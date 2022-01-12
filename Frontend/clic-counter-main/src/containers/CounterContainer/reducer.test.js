import reducer from './reducer';
import { incrementAction, decrementAction } from './action';

test('should return the initial state', () => {
    expect(reducer(undefined, {})).toEqual({
        counter: 0
    });
});

test('should increment the value by one', () => {
    const prevState = { counter: 1 };
    expect(reducer(prevState, incrementAction())).toEqual({
        counter: 2
    });
});

test('should decrement the value by one', () => {
    const prevState = { counter: 2 };
    expect(reducer(prevState, decrementAction())).toEqual({
        counter: 1
    });
});

test('should not decrement below zero', () => {
    const prevState = { counter: 0 };
    expect(reducer(prevState, decrementAction())).toEqual({
        counter: 0
    });
});