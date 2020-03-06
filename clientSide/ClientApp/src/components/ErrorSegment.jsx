import React from "react";
import { Segment, List } from "semantic-ui-react";

const ErrorSegment = ({ errors }) => {
  console.log(errors);
  return (
    <div>
      {errors !== [null] && errors !== undefined ? (
        <Segment color="red">
          {errors.Length === 1
            ? <p>{errors}</p>
            : Object.values(errors).map(error => (
                <div style={{ color: "red" }} key={error}>
                  <List.Item inverted="true" color="red">
                    {error}
                  </List.Item>
                </div>
              ))}
        </Segment>
      ) : (
        ""
      )}
    </div>
  );
};

export default ErrorSegment;
