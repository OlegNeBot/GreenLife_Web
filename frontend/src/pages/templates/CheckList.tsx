import React from 'react';
import Template from './Template';

export interface ComponentProps {
  name: string,
  total: number,
  num: number,
  checkListId: number,
  className?: string
};

const CheckList : React.FC<ComponentProps> = props => {
  return (
    <Template type='checklist' name={props.name} total={props.total} num={props.num} checkListId={props.checkListId} className={props.className}/>
  );
}

export default CheckList;