
import { Range } from 'rc-slider';
import _extends from 'babel-runtime/helpers/extends';
import * as utils from 'rc-slider/es/utils';

export default class CustomRange extends Range {
    trimAlignValue(v, handle) {
        var nextProps = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : {};
  
        var mergedProps = _extends({}, this.props, nextProps);
        var valInRange = utils.ensureValueInRange(v, mergedProps);
        var valNotConflict = this.ensureValueNotConflict(handle, nextProps.value, valInRange, mergedProps);
        return utils.ensureValuePrecision(valNotConflict, mergedProps);
      }

    ensureValueNotConflict(handle, vals, val, _ref) {
        var allowCross = _ref.allowCross,
            thershold = _ref.pushable;

        var state = this.state || {};
        
        handle = handle === undefined ? state.handle : handle;
        thershold = Number(thershold);
        /* eslint-disable eqeqeq */
        if (!allowCross && handle != null && vals !== undefined) {
            if (handle > 0 && val <= vals[handle - 1] + thershold) {
                return vals[handle - 1] + thershold;
            }
            if (handle < vals.length - 1 && val >= vals[handle + 1] - thershold) {
                return vals[handle + 1] + thershold;
            }
        }
        /* eslint-enable eqeqeq */
        return val;
    }
}