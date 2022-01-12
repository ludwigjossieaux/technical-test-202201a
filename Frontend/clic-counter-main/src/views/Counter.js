import React from 'react';

function Counter({counter, onIncrement, onDecrement}) {
  return (
    <div>
        <p>Vous avez cliqué {counter} fois</p>
        <button onClick={onDecrement}>Décrémente</button>
        <button onClick={onIncrement}>Incrémente</button>
    </div>
  );
};

export default Counter;