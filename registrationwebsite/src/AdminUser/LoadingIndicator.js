import React from "react";
import { css } from "@emotion/core";
import { BeatLoader} from "react-spinners";


// Can be a string as well. Need to ensure each key-value pair ends with ;
const override = css`
  display: block;
  margin: 0 auto;
  border-color: red;
`;

class LoadingIndicator extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      loading: props.isloading
    };
  }

  changeLoading = (shouldLoad) => {
      this.setState( { loading: shouldLoad} );
  };

  render() {
    return (
      <div className="sweet-loading">
        <BeatLoader
          css={override}
          size={20}
          color={"#123abc"}
          loading={this.state.loading}
        />
      </div>
    );
  }
}

export default LoadingIndicator