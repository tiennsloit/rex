import React, { Component } from 'react';
import { Text, View, DatePickerAndroid, TouchableWithoutFeedback, StyleSheet } from 'react-native';
class DatePicker extends Component {

  state = {
    presetDate: new Date(2020, 4, 5),
    allDate: new Date(2020, 4, 5),
    simpleDate: new Date(),
    simpleText: 'pick a date',
    spinnerText: 'pick a date',
    calendarText: 'pick a date',
    defaultText: 'pick a date',
    minText: 'pick a date, no earlier than today',
    maxText: 'pick a date, no later than today',
    presetText: 'pick a date, preset to 2020/5/5',
    allText: 'pick a date between 2020/5/1 and 2020/5/10',
  };

  showPicker = async (stateKey, options) => {
    try {
      var newState = {};
      const {action, year, month, day} = await DatePickerAndroid.open(options);
      if (action === DatePickerAndroid.dismissedAction) {
        newState[stateKey + 'Text'] = this.state.defaultText;
      } else {
        debugger;
        var date = new Date(year, month, day);
        newState[stateKey + 'Text'] = date.toLocaleDateString();
        newState[stateKey + 'Date'] = date;
        this.props.onValueChange(date);
      }
      this.setState(newState);
    } catch ({code, message}) {
      console.warn(`Error in example '${stateKey}': `, message);
    }
  };

  render() {
    return (
      <View style={styles.row}>
        <TouchableWithoutFeedback
          onPress={this.showPicker.bind(this, 'simple', { date: this.state.simpleDate })}>
          <Text style={styles.label}>{this.state.simpleText}</Text>
        </TouchableWithoutFeedback>
        <Text style={styles.input}></Text>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  text: {
    color: 'black',
    flex: 1
  },
  label: {
    flex: 0.75
  },
  row: {
        
        paddingLeft: 5
    },
  input: {
    flex: 0.25
  }
});

export default DatePicker;