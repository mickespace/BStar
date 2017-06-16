jQuery("#simulation")
  .on("click", ".s-b1b77cc9-428f-4aee-b00c-6269ce963b77 .click", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#s-Hotspot_7")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimNavigation",
                  "parameter": {
                    "target": "screens/cf6b6228-2440-41fd-8de9-111757ea3565"
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#s-Hotspot_8")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimNavigation",
                  "parameter": {
                    "target": "screens/1ad161e1-a218-4b9c-ad09-fce62a5f7b34"
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#s-Hotspot_13")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimNavigation",
                  "parameter": {
                    "target": "screens/c02b21a9-25d7-4105-96f2-412972a69caf"
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#s-Hotspot_4")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimNavigation",
                  "parameter": {
                    "target": "screens/d12245cc-1680-458d-89dd-4f0d7fb22724"
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#s-Hotspot_12")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": "#s-Group_5",
                    "effect": {
                      "type": "slide",
                      "duration": 200,
                      "direction": "up"
                    }
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#s-Hotspot_11")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimNavigation",
                  "parameter": {
                    "target": "screens/02f39a12-c9a5-4aa3-ab07-e377e6e28374"
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#s-Hotspot_5")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimNavigation",
                  "parameter": {
                    "target": "screens/c05e0ffb-66e3-4d62-8530-2b90fe28adb0"
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    }
  })
  .on("mouseenter dragenter", ".s-b1b77cc9-428f-4aee-b00c-6269ce963b77 .mouseenter", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getDirectEventFirer(this);
    if(jFirer.is("#s-Hotspot_7") && jFirer.has(event.relatedTarget).length === 0) {
      event.backupState = true;
      event.target = jFirer;
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_7": {
                      "attributes": {
                        "font-size": "10.0pt",
                        "font-family": "冬青黑体简体中文 W3,Arial"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_7 .valign": {
                      "attributes": {
                        "vertical-align": "middle",
                        "text-align": "left"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_7 span": {
                      "attributes": {
                        "color": "#017AFD",
                        "text-align": "left",
                        "text-decoration": "none",
                        "font-family": "冬青黑体简体中文 W3,Arial",
                        "font-size": "10.0pt",
                        "font-style": "normal",
                        "font-weight": "300"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        },
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Rectangle_23": {
                      "attributes": {
                        "border-top-width": "1px",
                        "border-top-style": "solid",
                        "border-top-color": "#017AFD",
                        "border-right-width": "1px",
                        "border-right-style": "solid",
                        "border-right-color": "#017AFD",
                        "border-bottom-width": "1px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#017AFD",
                        "border-left-width": "1px",
                        "border-left-style": "solid",
                        "border-left-color": "#017AFD",
                        "border-radius": "11px 11px 11px 11px"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Rectangle_23": {
                      "attributes-ie": {
                        "border-top-width": "1px",
                        "border-top-style": "solid",
                        "border-top-color": "#017AFD",
                        "border-right-width": "1px",
                        "border-right-style": "solid",
                        "border-right-color": "#017AFD",
                        "border-bottom-width": "1px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#017AFD",
                        "border-left-width": "1px",
                        "border-left-style": "solid",
                        "border-left-color": "#017AFD",
                        "border-radius": "11px 11px 11px 11px"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      jEvent.launchCases(cases);
    } else if(jFirer.is("#s-Hotspot_4") && jFirer.has(event.relatedTarget).length === 0) {
      event.backupState = true;
      event.target = jFirer;
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_8": {
                      "attributes": {
                        "font-size": "12.0pt",
                        "font-family": "冬青黑体简体中文 W3,Arial"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_8 .valign": {
                      "attributes": {
                        "vertical-align": "middle",
                        "text-align": "left"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_8 span": {
                      "attributes": {
                        "color": "#017AFD",
                        "text-align": "left",
                        "text-decoration": "none",
                        "font-family": "冬青黑体简体中文 W3,Arial",
                        "font-size": "12.0pt",
                        "font-style": "normal",
                        "font-weight": "300"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      jEvent.launchCases(cases);
    } else if(jFirer.is("#s-Hotspot_9") && jFirer.has(event.relatedTarget).length === 0) {
      event.backupState = true;
      event.target = jFirer;
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_10": {
                      "attributes": {
                        "font-size": "12.0pt",
                        "font-family": "冬青黑体简体中文 W3,Arial"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_10 .valign": {
                      "attributes": {
                        "vertical-align": "middle",
                        "text-align": "left"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_10 span": {
                      "attributes": {
                        "color": "#017AFD",
                        "text-align": "left",
                        "text-decoration": "none",
                        "font-family": "冬青黑体简体中文 W3,Arial",
                        "font-size": "12.0pt",
                        "font-style": "normal",
                        "font-weight": "300"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      jEvent.launchCases(cases);
    } else if(jFirer.is("#s-Hotspot_10") && jFirer.has(event.relatedTarget).length === 0) {
      event.backupState = true;
      event.target = jFirer;
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_11": {
                      "attributes": {
                        "font-size": "12.0pt",
                        "font-family": "冬青黑体简体中文 W3,Arial"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_11 .valign": {
                      "attributes": {
                        "vertical-align": "middle",
                        "text-align": "left"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_11 span": {
                      "attributes": {
                        "color": "#017AFD",
                        "text-align": "left",
                        "text-decoration": "none",
                        "font-family": "冬青黑体简体中文 W3,Arial",
                        "font-size": "12.0pt",
                        "font-style": "normal",
                        "font-weight": "300"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      jEvent.launchCases(cases);
    } else if(jFirer.is("#s-Hotspot_12") && jFirer.has(event.relatedTarget).length === 0) {
      event.backupState = true;
      event.target = jFirer;
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_15": {
                      "attributes": {
                        "font-size": "12.0pt",
                        "font-family": "冬青黑体简体中文 W3,Arial"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_15 .valign": {
                      "attributes": {
                        "vertical-align": "middle",
                        "text-align": "left"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_15 span": {
                      "attributes": {
                        "color": "#017AFD",
                        "text-align": "left",
                        "text-decoration": "none",
                        "font-family": "冬青黑体简体中文 W3,Arial",
                        "font-size": "12.0pt",
                        "font-style": "normal",
                        "font-weight": "300"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        },
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Triangle_12 .shapert-clipping": {
                      "attributes": {
                        "left": "0px",
                        "top": "0px",
                        "width": "10px",
                        "height": "6px",
                        "position": "absolute",
                        "overflow": "hidden"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Triangle_12 .content": {
                      "attributes": {
                        "width": "10px",
                        "height": "6px"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Triangle_12": {
                      "attributes": {
                        "background-color": "#017AFD",
                        "background-image": "none"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Triangle_12": {
                      "attributes-ie": {
                        "-pie-background": "#017AFD",
                        "-pie-poll": "false"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      jEvent.launchCases(cases);
    } else if(jFirer.is("#s-Hotspot_11") && jFirer.has(event.relatedTarget).length === 0) {
      event.backupState = true;
      event.target = jFirer;
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_15": {
                      "attributes": {
                        "font-size": "12.0pt",
                        "font-family": "冬青黑体简体中文 W3,Arial"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_15 .valign": {
                      "attributes": {
                        "vertical-align": "middle",
                        "text-align": "left"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_15 span": {
                      "attributes": {
                        "color": "#017AFD",
                        "text-align": "left",
                        "text-decoration": "none",
                        "font-family": "冬青黑体简体中文 W3,Arial",
                        "font-size": "12.0pt",
                        "font-style": "normal",
                        "font-weight": "300"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        },
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Triangle_12 .shapert-clipping": {
                      "attributes": {
                        "left": "0px",
                        "top": "0px",
                        "width": "10px",
                        "height": "6px",
                        "position": "absolute",
                        "overflow": "hidden"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Triangle_12 .content": {
                      "attributes": {
                        "width": "10px",
                        "height": "6px"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Triangle_12": {
                      "attributes": {
                        "background-color": "#017AFD",
                        "background-image": "none"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Triangle_12": {
                      "attributes-ie": {
                        "-pie-background": "#017AFD",
                        "-pie-poll": "false"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      jEvent.launchCases(cases);
    } else if(jFirer.is("#s-Hotspot_5") && jFirer.has(event.relatedTarget).length === 0) {
      event.backupState = true;
      event.target = jFirer;
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_9": {
                      "attributes": {
                        "font-size": "12.0pt",
                        "font-family": "冬青黑体简体中文 W3,Arial"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_9 .valign": {
                      "attributes": {
                        "vertical-align": "middle",
                        "text-align": "left"
                      }
                    }
                  },{
                    "#s-b1b77cc9-428f-4aee-b00c-6269ce963b77 #s-Text_9 span": {
                      "attributes": {
                        "color": "#017AFD",
                        "text-align": "left",
                        "text-decoration": "none",
                        "font-family": "冬青黑体简体中文 W3,Arial",
                        "font-size": "12.0pt",
                        "font-style": "normal",
                        "font-weight": "300"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      jEvent.launchCases(cases);
    }
  })
  .on("mouseleave dragleave", ".s-b1b77cc9-428f-4aee-b00c-6269ce963b77 .mouseleave", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getDirectEventFirer(this);
    if(jFirer.is("#s-Hotspot_7")) {
      jEvent.undoCases(jFirer);
    } else if(jFirer.is("#s-Hotspot_4")) {
      jEvent.undoCases(jFirer);
    } else if(jFirer.is("#s-Hotspot_9")) {
      jEvent.undoCases(jFirer);
    } else if(jFirer.is("#s-Hotspot_10")) {
      jEvent.undoCases(jFirer);
    } else if(jFirer.is("#s-Hotspot_12")) {
      jEvent.undoCases(jFirer);
    } else if(jFirer.is("#s-Hotspot_11")) {
      jEvent.undoCases(jFirer);
    } else if(jFirer.is("#s-Hotspot_5")) {
      jEvent.undoCases(jFirer);
    }
  });