-- A2A Cessna 172R --


-- ## Cabin lights ###############

function A172_FloodLight_dim ()
      ipc.setbitsUW("0D0C", 512)
      ipc.clearbitsUW("0D0C", 256)
      DspShow ("Flod", "Dim", " Flood", "  Dim")
end

function A172_FloodLight_bright ()
      ipc.setbitsUW("0D0C", 512)
      ipc.setbitsUW("0D0C", 256)
      DspShow ("Flod", " On", " Flood", " Bright")
end

function A172_FloodLight_off ()
      ipc.clearbitsUW("0D0C", 512)
      ipc.clearbitsUW("0D0C", 256)
      DspShow ("Flod", " Off", " Flood", "  Off")
end

function A172_FloodLight_toggle ()

     local l = ipc.readLvar("FloodLight1Switch") + ipc.readLvar("FloodLight2Switch")

     if l == 0 then
         A172_FloodLight_dim ()
     elseif l == 1 then
         A172_FloodLight_bright ()
     else
         A172_FloodLight_off ()
     end
end

function A172_InstrLight_off ()
    ipc.writeLvar('PanelLightKnob', 0)
    ipc.writeLvar('RadioLightKnob', 0)
    DspShow ("INSR", " Off", "Instrum", "  Off")
end

function A172_InstrLight_dim ()
    ipc.writeLvar('PanelLightKnob', 5)
    ipc.writeLvar('RadioLightKnob', 5)
    DspShow ("INSR", " Off", "Instrum", "  Off")
end

function A172_InstrLight_bright ()
    ipc.writeLvar('PanelLightKnob', 15)
    ipc.writeLvar('RadioLightKnob', 15)
    DspShow ("INSR", " Off", "Instrum", "  Off")
end

function A172_InstrLight_toggle ()

    local l = ipc.readLvar('PanelLightKnob')

    if l == 0 then

        A172_InstrLight_dim ()

    elseif l == 5 then

        A172_InstrLight_bright ()

    else

        A172_InstrLight_off ()

    end

end


function A172_PanelsLight_off ()
    ipc.writeLvar('GlareshieldLightKnob', 0)
    ipc.writeLvar('PedestalLightKnob', 0)
    DspShow ("PANL", " Off", " Panel", "  Off")
end

function A172_PanelsLight_dim ()
    ipc.writeLvar('GlareshieldLightKnob', 5)
    ipc.writeLvar('PedestalLightKnob', 5)
    DspShow ("PANL", " Off", " Panel", "  Off")
end

function A172_PanelsLight_bright ()
    ipc.writeLvar('GlareshieldLightKnob', 15)
    ipc.writeLvar('PedestalLightKnob', 15)
    DspShow ("PANL", " Off", " Panel", "  Off")
end

function A172_PanelsLight_toggle ()

    local l = ipc.readLvar('GlareshieldLightKnob')

    if l == 0 then

        A172_PanelsLight_dim ()

    elseif l == 5 then

        A172_PanelsLight_bright ()

    else

        A172_PanelsLight_off ()

    end

end



-- ## Light knobs ###############

function A172_InstrLight_show ()

    local r = round(ipc.readLvar("PanelLightKnob")*3.14)
    DspShow ("INSR", r .. "%", "Instrum", "  " .. r .. "%")

end

function A172_InstrLight_plus ()

    A172_GaugesLight_plus ()
    A172_RadioLight_plus ()
    A172_InstrLight_show ()

end

function A172_InstrLight_plusfast ()

    A172_GaugesLight_plusfast ()
    A172_RadioLight_plusfast ()
    A172_InstrLight_show ()

end

function A172_InstrLight_minus ()

    A172_GaugesLight_minus ()
    A172_RadioLight_minus ()
    A172_InstrLight_show ()

end

function A172_InstrLight_minusfast ()

    A172_GaugesLight_minusfast ()
    A172_RadioLight_minusfast ()
    A172_InstrLight_show ()

end

function A172_PanelsLight_show ()

    local r = round(ipc.readLvar("GlareshieldLightKnob")*3.14)
    DspShow ("PANL", r .. "%", " Panel", "  " .. r .. "%")

end

function A172_PanelsLight_plus ()

    A172_GlareshieldLight_plus ()
    A172_PedestalLight_plus ()
    A172_PanelsLight_show ()

end

function A172_PanelsLight_plusfast ()

    A172_GlareshieldLight_plusfast ()
    A172_PedestalLight_plusfast ()
    A172_PanelsLight_show ()

end

function A172_PanelsLight_minus ()

    A172_GlareshieldLight_minus ()
    A172_PedestalLight_minus ()
    A172_PanelsLight_show ()

end

function A172_PanelsLight_minusfast ()

    A172_GlareshieldLight_minusfast ()
    A172_PedestalLight_minusfast ()
    A172_PanelsLight_show ()

end

function A172_GaugesLight_plus ()

    local pl = ipc.readLvar('PanelLightKnob')
    if pl < 32 then
        pl = pl + 1
    end
    ipc.writeLvar('PanelLightKnob', pl)

end

function A172_GaugesLight_plusfast ()

    local pl = ipc.readLvar('PanelLightKnob')
    if pl < 32 then
        pl = pl + 4
    end
    if pl > 32 then
        pl = 32
    end
    ipc.writeLvar('PanelLightKnob', pl)

end


function A172_GaugesLight_minus ()

    local pl = ipc.readLvar('PanelLightKnob')
    if pl > 0 then
        pl = pl - 1
    end
    ipc.writeLvar('PanelLightKnob', pl)

end

function A172_GaugesLight_minusfast ()

    local pl = ipc.readLvar('PanelLightKnob')
    if pl > 0 then
        pl = pl - 4
    end
    if pl < 0 then
        pl = 0
    end
    ipc.writeLvar('PanelLightKnob', pl)

end


function A172_RadioLight_plus ()

    local pl = ipc.readLvar('RadioLightKnob')
    if pl < 32 then
        pl = pl + 1
    end
    ipc.writeLvar('RadioLightKnob', pl)

end

function A172_RadioLight_plusfast ()

    local pl = ipc.readLvar('RadioLightKnob')
    if pl < 32 then
        pl = pl + 4
    end
    if pl > 32 then
        pl = 32
    end
    ipc.writeLvar('RadioLightKnob', pl)

end


function A172_RadioLight_minus ()

    local pl = ipc.readLvar('RadioLightKnob')
    if pl > 0 then
        pl = pl - 1
    end
    ipc.writeLvar('RadioLightKnob', pl)

end

function A172_RadioLight_minusfast ()

    local pl = ipc.readLvar('RadioLightKnob')
    if pl > 0 then
        pl = pl - 4
    end
    if pl < 0 then
        pl = 0
    end
    ipc.writeLvar('RadioLightKnob', pl)

end

function A172_GlareshieldLight_plus ()

    local pl = ipc.readLvar('GlareshieldLightKnob')
    if pl < 32 then
        pl = pl + 1
    end
    ipc.writeLvar('GlareshieldLightKnob', pl)

end

function A172_GlareshieldLight_plusfast ()

    local pl = ipc.readLvar('GlareshieldLightKnob')
    if pl < 32 then
        pl = pl + 4
    end
    if pl > 32 then
        pl = 32
    end
    ipc.writeLvar('GlareshieldLightKnob', pl)

end


function A172_GlareshieldLight_minus ()

    local pl = ipc.readLvar('GlareshieldLightKnob')
    if pl > 0 then
        pl = pl - 1
    end
    ipc.writeLvar('GlareshieldLightKnob', pl)

end

function A172_GlareshieldLight_minusfast ()

    local pl = ipc.readLvar('GlareshieldLightKnob')
    if pl > 0 then
        pl = pl - 4
    end
    if pl < 0 then
        pl = 0
    end
    ipc.writeLvar('GlareshieldLightKnob', pl)

end

function A172_PedestalLight_plus ()

    local pl = ipc.readLvar('PedestalLightKnob')
    if pl < 32 then
        pl = pl + 1
    end
    ipc.writeLvar('PedestalLightKnob', pl)

end

function A172_PedestalLight_plusfast ()

    local pl = ipc.readLvar('PedestalLightKnob')
    if pl < 32 then
        pl = pl + 4
    end
    if pl > 32 then
        pl = 32
    end
    ipc.writeLvar('PedestalLightKnob', pl)

end


function A172_PedestalLight_minus ()

    local pl = ipc.readLvar('PedestalLightKnob')
    if pl > 0 then
        pl = pl - 1
    end
    ipc.writeLvar('PedestalLightKnob', pl)

end

function A172_PedestalLight_minusfast ()

    local pl = ipc.readLvar('PedestalLightKnob')
    if pl > 0 then
        pl = pl - 4
    end
    if pl < 0 then
        pl = 0
    end
    ipc.writeLvar('PedestalLightKnob', pl)

end


-- ## Electric Switches ###############

function A172_Magneto_start ()

    _MAGNETO1_SET (4)
    DspShow ("MAG", "Strt", "Magneto", " Start")

end

function A172_Magneto_both ()

    _MAGNETO1_SET (3)
    DspShow ("MAG", "both", "Magneto", " both")

end

function A172_Magneto_left ()

    _MAGNETO1_SET (2)
    DspShow ("MAG", "left", "Magneto", " left")

end

function A172_Magneto_right ()

    _MAGNETO1_SET (1)
    DspShow ("MAG", "rght", "Magneto", " right")

end

function A172_Magneto_stop ()

    _MAGNETO1_SET (0)
    DspShow ("MAG", "Stop", "Magneto", " Stop")

end

function A172_Magneto_inc ()

    local t = ipc.readLvar('Magnetos1')
    if t < 4 then t = t + 1 end
    _MAGNETO1_SET (t)

    if t == 0 then
        DspShow ("MAG", "Off")
    elseif t == 1 then
        DspShow ("MAG", "Rght")
    elseif t == 2 then
        DspShow ("MAG", "Left")
    elseif t == 3 then
        DspShow ("MAG", "Both")
    elseif t == 4 then
        DspShow ("MAG", "Strt")
    end

    _sleep(100)

end

function A172_Magneto_dec ()

    local t = ipc.readLvar('Magnetos1')
    if t > 0 then t = t - 1 end
    _MAGNETO1_SET (t)

    if t == 0 then
        DspShow ("MAG", "Off")
    elseif t == 1 then
        DspShow ("MAG", "Rght")
    elseif t == 2 then
        DspShow ("MAG", "Left")
    elseif t == 3 then
        DspShow ("MAG", "Both")
    elseif t == 4 then
        DspShow ("MAG", "Strt")
    end

    _sleep(100)

end


function A172_AnnLights_test ()

    local i
    ipc.writeLvar('AnnPanelTestSwitchDrag', 1)
    ipc.writeLvar('AnnPanelTestSwitchPercent', 100)
    for i = 0,70,1 do
        ipc.writeLvar('AnnunciatorPanelTestSwitch', 2)
    end
    ipc.writeLvar('AnnPanelTestSwitchDrag', 0)

end

function A172_AnnLights_dim ()

    ipc.writeLvar('AnnunciatorPanelTestSwitch', 0)
    ipc.writeLvar('AnnPanelTestSwitchPercent', 0)
end

function A172_AnnLights_bright ()

    ipc.writeLvar('AnnunciatorPanelTestSwitch', 1)
    ipc.writeLvar('AnnPanelTestSwitchPercent', 50)
end

function A172_AnnLights_toggle ()

    if ipc.readLvar('AnnunciatorPanelTestSwitch') == 0 then

        A172_AnnLights_test ()

    elseif ipc.readLvar('AnnunciatorPanelTestSwitch') == 1 then

        A172_AnnLights_dim ()

    else

        A172_AnnLights_bright ()

    end
end

function A172_Alternator_on ()

    ipc.writeLvar('Eng1_GeneratorSwitch', 1)
    DspShow ("GEN", "ON")

end

function A172_Alternator_off ()

    ipc.writeLvar('Eng1_GeneratorSwitch', 0)
    DspShow ("GEN", "OFF")

end

function A172_Alternator_toggle ()

	if ipc.readLvar('Eng1_GeneratorSwitch') == 0 then

        A172_Alternator_on ()

	else

        A172_Alternator_off ()

	end

end


function A172_Battery_on ()

    ipc.writeLvar('Battery1Switch', 1)
    DspShow ("BATT", "ON")

end

function A172_Battery_off ()

    ipc.writeLvar('Battery1Switch', 0)
    DspShow ("BATT", "OFF")

end

function A172_Battery_toggle ()

	if ipc.readLvar('Battery1Switch') == 0 then

        A172_Battery_on ()

	else

        A172_Battery_off ()

	end

end

function A172_Avionics_on ()

    _AVIONICS_MASTER_SET (1)
    DspShow ("MAST", "ON")

end

function A172_Avionics_off ()

    _AVIONICS_MASTER_SET (0)
    DspShow ("MAST", "OFF")

end

function A172_Avionics_toggle ()

	if ipc.readDD(0x2E80) == 0 then

        A172_Avionics_on ()

	else

        A172_Avionics_off ()

	end

end


function A172_FuelPump_on ()

    ALL_FuelPumps_on ()

end

function A172_FuelPump_off ()

    ALL_FuelPumps_off ()

end

function A172_FuelPump_toggle ()

    ALL_FuelPumps_toggle ()

end


function A172_BeaconLight_on ()

    Lights_BEACON_on ()

end

function A172_BeaconLight_off ()

    Lights_BEACON_off ()

end

function A172_BeaconLight_toggle ()

    Lights_BEACON_toggle ()

end



function A172_LandingLight_on ()

    Lights_LANDING_on ()

end

function A172_LandingLight_off ()

    Lights_LANDING_off ()

end

function A172_LandingLight_toggle ()

    Lights_LANDING_toggle ()

end

function A172_TaxiLight_on ()

    Lights_TAXI_on ()

end

function A172_TaxiLight_off ()

    Lights_TAXI_off ()

end

function A172_TaxiLight_toggle ()

    Lights_TAXI_toggle ()

end

function A172_NavLight_on ()

    Lights_NAV_on ()

end

function A172_NavLight_off ()

    Lights_NAV_off ()

end

function A172_NavLight_toggle ()

    Lights_NAV_toggle ()

end

function A172_StrobeLight_on ()

    Lights_STROBE_on ()

end

function A172_StrobeLight_off ()

    Lights_STROBE_off ()

end

function A172_StrobeLight_toggle ()

    Lights_STROBE_toggle ()

end

function A172_PitotHeat_on ()

    DeIce_PITOT_on ()

end

function A172_PitotHeat_off ()

    DeIce_PITOT_off ()

end

function A172_PitotHeat_toggle ()

    DeIce_PITOT_toggle ()

end

-- ## GPS Controls ###############

function A172_GPS_on ()
      ipc.writeLvar("GpsOnSwitch", 1)
      DspShow ("GPS", "On")
end

function A172_GPS_off ()
      ipc.writeLvar("GpsOnSwitch", 0)
      DspShow ("GPS", "Off")
end

function A172_GPS_toggle ()
	if _tl("GpsOnSwitch", 0) then
      A172_GPS_on ()
	else
      A172_GPS_off ()
	end
end


-- ## Autopilot knobs ###############

function A172_AP_HDG_plus (a, b, c)

    HDG_plus (a, b, c)
    if _MCP2 () then DspHDG(HDG) end

end

function A172_AP_HDG_plusfast (a, b, c)

    HDG_plusfast (a, b, c)
    if _MCP2 () then DspHDG(HDG) end

end


function A172_AP_HDG_minus (a, b, c)

    HDG_minus (a, b, c)
    if _MCP2 () then DspHDG(HDG) end

end

function A172_AP_HDG_minusfast (a, b, c)

    HDG_minusfast (a, b, c)
    if _MCP2 () then DspHDG(HDG) end

end

function A172_AP_ALT_plus (a, b, c)

    --if c == nil then
        A172_AP_InnerKnob_plus ()
    --else
    --    if _MCP2 () then
    --        ipc.writeLvar("AutopilotAltAlerter", c * 100)
    --        DspALT(ALT)
    --    else
    --        ipc.writeLvar("AutopilotAltAlerter", c * 10)
    --    end
    --end

end

function A172_AP_ALT_plusfast (a, b, c)

    --if c == nil then
        A172_AP_OuterKnob_plus ()
    --else
    --    if _MCP2 () then
    --        ipc.writeLvar("AutopilotAltAlerter", c * 100)
    --        DspALT(ALT)
    --   else
    --        ipc.writeLvar("AutopilotAltAlerter", c * 10)
    --    end
    --end

end

function A172_AP_ALT_minus (a, b, c)

    --if c == nil then
        A172_AP_InnerKnob_minus ()
    --else
    --    if _MCP2 () then
    --        ipc.writeLvar("AutopilotAltAlerter", c * 100)
    --        DspALT(ALT)
    --    else
    --        ipc.writeLvar("AutopilotAltAlerter", c * 10)
    --    end
    --end

end

function A172_AP_ALT_minusfast (a, b, c)

    --if c == nil then
        A172_AP_OuterKnob_minus ()
    --else
    --    if _MCP2 () then
    --        ipc.writeLvar("AutopilotAltAlerter", c * 100)
    --        DspALT(ALT)
    --    else
    --        ipc.writeLvar("AutopilotAltAlerter", c * 10)
    --    end
    --end

end

function A172_AP_VS_plus (a, b, c)

    --if c == nil then
        A172_AP_Button_up ()
    --else
    --    if _MCP2 () then
    --        if c > 15 then c = 15 end
    --        VVS_plus (a, b, c)
    --        DspVVS(c)
    --    else
    --        VVS_plus ()
    --    end
    --end

end

function A172_AP_VS_minus (a, b, c)

    --if c == nil then
        A172_AP_Button_dn ()
    --else
    --    if _MCP2 () then
    --        if c < -20 then c = -20 end
    --        VVS_minus (a, b, c)
    --        DspVVS(c)
    --    else
    --        VVS_minus ()
    --    end
    --end

end

-- ## Autopilot buttons ###############

function A172_AP_Button_hdg ()
      ipc.writeLvar("kap140_hdg_button", 1)
      ipc.writeLvar("kap140_hdg", 1)
      --DspShow ("Hdg", "hold")
      _sleep(50,150)
      ipc.writeLvar("kap140_hdg_button", 0)
end

function A172_AP_Button_nav ()
    ipc.writeLvar("kap140_nav_button", 1)
    ipc.writeLvar("kap140_nav", 1)
    --DspShow ("nav", "hold")
    _sleep(50,150)
    ipc.writeLvar("kap140_nav_button", 0)

      -- blink HDG
    if dev == 0 then return end

   	ipc.sleep(30)
    com.write(dev, "DSP4    " .. s, 8)
   	ipc.sleep(300)
    com.write(dev, "DSP4HDG " .. s, 8)
   	ipc.sleep(300)
    com.write(dev, "DSP4    " .. s, 8)
   	ipc.sleep(300)
    com.write(dev, "DSP4HDG " .. s, 8)
   	ipc.sleep(300)
    com.write(dev, "DSP4    " .. s, 8)
   	ipc.sleep(300)
    com.write(dev, "DSP4HDG " .. s, 8)

    DspHideCursor ()

end

function A172_AP_Button_apr ()
      ipc.writeLvar("kap140_apr_button", 1)
      ipc.writeLvar("kap140_apr", 1)
      --DspShow ("apr", "hold")
      _sleep(50,150)
      ipc.writeLvar("kap140_apr_button", 0)
end

function A172_AP_Button_rev ()
      ipc.writeLvar("kap140_rev_button", 1)
      ipc.writeLvar("kap140_rev", 1)
      --DspShow ("rev", "hold")
      _sleep(50,150)
      ipc.writeLvar("kap140_rev_button", 0)
end

function A172_AP_Button_alt ()
      ipc.writeLvar("kap140_alt_button", 1)
      ipc.writeLvar("kap140_alt", 1)
      -- _AP_VS_VAR_SET_ENGLISH(1)
      --DspShow ("Alt", "hold")
      _sleep(50,150)
      ipc.writeLvar("kap140_alt_button", 0)
end

function A172_AP_Button_arm ()
      ipc.writeLvar("kap140_arm_button", 1)
      ipc.writeLvar("kap140_arm", 1)
      --DspShow ("Alt", "arm")
      _sleep(50,150)
      ipc.writeLvar("kap140_arm_button", 0)
end


function A172_AP_Button_baro ()
      ipc.writeLvar("kap140_baro_button", 1)
      ipc.writeLvar("kap140_baro", 1)
      --DspShow ("baro", "hold")
      _sleep(50,150)
      ipc.writeLvar("kap140_baro_button", 0)
end

function A172_AP_Button_up ()
      ipc.writeLvar("kap140_up_button", 1)
      DspShow ("Vs", "up")
      _sleep(50,150)
      ipc.writeLvar("kap140_up_button", 0)
end

function A172_AP_Button_dn ()
      ipc.writeLvar("kap140_dn_button", 1)
      DspShow ("Vs", "dn")
      _sleep(50,150)
      ipc.writeLvar("kap140_dn_button", 0)
end

function A172_AP_InnerKnob_plus ()
      ipc.writeLvar("kap140_InnerKnob", 1)
      DspALT(ipc.readLvar("AutopilotAltAlerter")/100)
end

function A172_AP_InnerKnob_minus ()
      ipc.writeLvar("kap140_InnerKnob", -1)
      DspALT(ipc.readLvar("AutopilotAltAlerter")/100)
end

function A172_AP_OuterKnob_plus ()
      ipc.writeLvar("kap140_OuterKnob", 1)
      DspALT(ipc.readLvar("AutopilotAltAlerter")/100)
end

function A172_AP_OuterKnob_minus ()
      ipc.writeLvar("kap140_OuterKnob", -1)
      DspALT(ipc.readLvar("AutopilotAltAlerter")/100)
end


-- ## NAV ###############

function A172_Nav1Ident_on  ()

    _RADIO_VOR1_IDENT_ENABLE ()
    ipc.writeLvar('kma26Nav1Switch', 1)
    ipc.writeLvar('Nav1PullIdent', 1)

    if _MCP1 () then
        DspShow("NAV1","AUD")
    else
        DspRadioIdent_on ()
    end

end

function A172_Nav1Ident_off  ()

    _RADIO_VOR1_IDENT_DISABLE ()
    ipc.writeLvar('kma26Nav1Switch', 0)
    ipc.writeLvar('Nav1PullIdent', 0)

    if _MCP1 () then
        DspShow("NAV1","OFF")
    else
        DspRadioIdent_off ()
    end

end

function A172_Nav1Ident_toggle  ()

	if ipc.readLvar('Nav1PullIdent') == 1 then
        A172_Nav1Ident_off  ()
	else
        A172_Nav1Ident_on  ()
	end

end


function A172_Nav2Ident_on  ()

    _RADIO_VOR2_IDENT_ENABLE ()
    ipc.writeLvar('kma26Nav2Switch', 1)
    ipc.writeLvar('Nav2PullIdent', 1)

    if _MCP1 () then
        DspShow("NAV2","AUD")
    else
        DspRadioIdent_on ()
    end

end

function A172_Nav2Ident_off  ()

    _RADIO_VOR2_IDENT_DISABLE ()
    ipc.writeLvar('kma26Nav2Switch', 0)
    ipc.writeLvar('Nav2PullIdent', 0)

    if _MCP1 () then
        DspShow("NAV2","OFF")
    else
        DspRadioIdent_off ()
    end

end

function A172_Nav2Ident_toggle ()

	if ipc.readLvar('Nav2PullIdent') == 1 then
        A172_Nav2Ident_off  ()
	else
        A172_Nav2Ident_on  ()
	end

end


function A172_NavIdent_toggle ()

	if nav_sel == 1 then
        A172_Nav1Ident_toggle ()
	else
        A172_Nav2Ident_toggle ()
	end

end


-- ## ADF ###############

function A172_ADFbuttonAdf_on ()

    ipc.writeLvar("AdfAdfButton", 1)

end

function A172_ADFbuttonAdf_off ()

    ipc.writeLvar("AdfAdfButton", 0)

end

function A172_ADFbuttonAdf_toggle ()

    local b = ipc.readLvar("AdfAdfButton")
    b = 1 - b
    ipc.writeLvar("AdfAdfButton", b)

end

function A172_ADFbuttonBfo_on ()

    ipc.writeLvar("AdfBfoButton", 1)

    if _MCP1 () then
        DspShow ("ADF", "BFO")
    else
        if RADIOS_MODE == 3 then
            DspRadioShort("BFO")
        else
            DspShow ("", "", "  ADF", " BFO on")
        end
    end

end

function A172_ADFbuttonBfo_off ()

    ipc.writeLvar("AdfBfoButton", 0)

    if _MCP1 () then
        DspShow ("ADF", "BFO")
    else
        if RADIOS_MODE == 3 then
            DspRadioShort("")
        else
            DspShow ("", "", "  ADF", " BFO off")
        end
    end

end

function A172_ADFbuttonBfo_toggle ()

    local b = ipc.readLvar("AdfBfoButton")

    if b == 1 then

        A172_ADFbuttonBfo_off ()

    else

        A172_ADFbuttonBfo_on ()

    end
end

function A172_ADFbuttonFrq_click ()

    ipc.writeLvar("AdfFrqButton", 1)
    if _MCP1 () then
        DspShow ("ADF", "<->")
    end
    _sleep(100,250)
    ipc.writeLvar("AdfFrqButton", 0)
    _sleep(50)

    if RADIOS_MODE == 3 then
        Default_ADF_1_init(true)
    end
end

function A172_ADFbuttonEt_click ()

    ipc.writeLvar("AdfFrqEtButton", 1)
    if _MCP1 () then
        DspShow ("ADF", " ET ")
    end
    _sleep(100,250)
    ipc.writeLvar("AdfFrqEtButton", 0)

end

function A172_ADFbuttonReset_click ()

    ipc.writeLvar("AdfFrqRstButton", 1)
    if _MCP1 () then
        DspShow ("ADF", "SET")
    end
    _sleep(100,250)
    ipc.writeLvar("AdfFrqRstButton", 0)

end

function A172_ADFbuttonReset_hold ()

    ipc.writeLvar("AdfFrqRstButton", 1)
    if _MCP1 () then
        DspShow ("ADF", "SET")
    end
    _sleep(2000,2500)
    ipc.writeLvar("AdfFrqRstButton", 0)

end

function A172_ADFident_on  ()

    ipc.control(65841)
    A172_ADFbuttonAdf_off ()

    if _MCP1 () then
        DspShow("ADF1","AUD")
    else
        DspRadioIdent_on ()
    end

end

function A172_ADFident_off  ()

    ipc.control(65836)
    A172_ADFbuttonAdf_on ()

    if _MCP1 () then
        DspShow("ADF1","OFF")
    else
        DspRadioIdent_off ()
    end

end

function A172_ADFident_toggle  ()

	if ipc.readLvar("AdfAdfButton") == 0 then
        A172_ADFident_off  ()
	else
        A172_ADFident_on  ()
	end

end

-- ## Transponder ###############

function A172_Transponder_cycle ()

    local x = ipc.readLvar("xpdr_onoff_knob_pos")
    x = x + 1
    if x > 4 then x = 0 end
    ipc.writeLvar("xpdr_onoff_knob_pos", x)
    A172_Transponder_show ()

end

function A172_Transponder_inc ()

    local x = ipc.readLvar("xpdr_onoff_knob_pos")
    x = x + 1
    if x > 4 then x = 4 end
    ipc.writeLvar("xpdr_onoff_knob_pos", x)
    A172_Transponder_show ()
end

function A172_Transponder_dec ()

    local x = ipc.readLvar("xpdr_onoff_knob_pos")
    x = x - 1
    if x < 0 then x = 0 end
    ipc.writeLvar("xpdr_onoff_knob_pos", x)
    A172_Transponder_show ()
end

function A172_Transponder_off ()

    ipc.writeLvar("xpdr_onoff_knob_pos", 0)
    A172_Transponder_show ()
end

function A172_Transponder_stby ()

    ipc.writeLvar("xpdr_onoff_knob_pos", 1)
    A172_Transponder_show ()
end


function A172_Transponder_test ()

    ipc.writeLvar("xpdr_onoff_knob_pos", 2)
    A172_Transponder_show ()
end

function A172_Transponder_on ()

    ipc.writeLvar("xpdr_onoff_knob_pos", 3)
    A172_Transponder_show ()
end

function A172_Transponder_alt ()

    ipc.writeLvar("xpdr_onoff_knob_pos", 4)
    A172_Transponder_show ()
end

function A172_Transponder_show ()

    local x = ipc.readLvar("xpdr_onoff_knob_pos")
    local xt, xtm

    if x == 0 then
        xt = "Off"
        xtm = "Off"
    elseif x == 1 then
        xt = "Stby"
        xtm = "Stby"
    elseif x == 2 then
        xt = "Test"
        xtm = "Test"
    elseif x == 3 then
        xt = "On"
        xtm = "On"
    elseif x == 4 then
        xt = "Alt"
        xtm = "Alt"
    end

    if _MCP1() then
        DspShow("XPDR", xt)
    else
        DspRadioShort(xtm)
    end

    DspRadioHideCursor ()
end


-- ## Other controls ###############

function A172_FuelSelector_show ()

    local t = ipc.readLvar('FSelC172State')

    if t == 0 then
        DspShow ("FUEL", "Left")
    elseif t == 1 then
        DspShow ("FUEL", "Both")
    elseif t == 2 then
        DspShow ("FUEL", "Rght", "  FUEL", "  Right")
    end

end

function A172_FuelSelector_inc ()

    local t = ipc.readLvar('FSelC172State')
    t = t + 1
    if t > 2 then t = 2 end
    ipc.writeLvar('FSelC172State', t)

    A172_FuelSelector_show ()
end

function A172_FuelSelector_dec ()

    local t = ipc.readLvar('FSelC172State')
    t = t - 1
    if t < 0 then t = 0 end
    ipc.writeLvar('FSelC172State', t)

    A172_FuelSelector_show ()
end

function A172_FuelSelector_left ()

    ipc.writeLvar('FSelC172State', 0)

    A172_FuelSelector_show ()
end

function A172_FuelSelector_right ()

    ipc.writeLvar('FSelC172State', 2)

    A172_FuelSelector_show ()
end

function A172_FuelSelector_both ()

    ipc.writeLvar('FSelC172State', 1)

    A172_FuelSelector_show ()
end

function A172_FuelSelector_toggle ()

    local f = ipc.readLvar('FSelC172State')
    f = f + 1
    if f > 2 then f = 0 end
    if f == 0 then
        A172_FuelSelector_left ()
    elseif f == 1 then
        A172_FuelSelector_both ()
    elseif f == 2 then
        A172_FuelSelector_right ()
    end

end


function A172_FuelCutoff_open ()

    ipc.writeLvar('Eng1_FuelCutOffSwitch', 1)
    if not QUIET then DspShow ("FUEL", "Open", " FuelCut", "  Open") end
    DspShort2 ("F ")

end

function A172_FuelCutoff_close ()

    ipc.writeLvar('Eng1_FuelCutOffSwitch', 0)
    if not QUIET then DspShow ("FUEL", "Shut", " FuelCut", " Closed") end
    DspShort2 ("F*")

end

function A172_FuelCutoff_toggle ()

    if ipc.readLvar('Eng1_FuelCutOffSwitch') == 0 then
        A172_FuelCutoff_open ()
    else
        A172_FuelCutoff_close ()
    end

end

function A172_AltStaticAir_on ()

    ipc.writeLvar('StaticAir', 1)
    if not QUIET then DspShow ("AIR", "Alt", " S.Air", " Alter") end
    DspShort1 ("A*")

end

function A172_AltStaticAir_off ()

    ipc.writeLvar('StaticAir', 0)
    if not QUIET then DspShow ("AIR", "Norm", " S.Air", " Normal") end
    DspShort1 ("A ")

end

function A172_AltStaticAir_toggle ()

    if ipc.readLvar('StaticAir') == 0 then
        A172_AltStaticAir_on ()
    else
        A172_AltStaticAir_off ()
    end

end


function A172_TempUnits_toggle ()

    temp_units = 1 - temp_units
    AdditionalInfo ()

end

function A172_EGTref_inc ()

    local e = ipc.readLvar('EGTGauge_ref')

    e = e - 1
    if e < 0 then e = 0 end

    ipc.writeLvar('EGTGauge_ref', e)
    A172_EGTref_show ()

end

function A172_EGTref_incfast ()

    local e = ipc.readLvar('EGTGauge_ref')

    e = e - 10
    if e < 0 then e = 0 end

    ipc.writeLvar('EGTGauge_ref', e)
    A172_EGTref_show ()

end

function A172_EGTref_dec ()


    local e = ipc.readLvar('EGTGauge_ref')

    e = e + 1
    if e > 100 then e = 100 end

    ipc.writeLvar('EGTGauge_ref', e)
    A172_EGTref_show ()

end

function A172_EGTref_decfast ()

    local e = ipc.readLvar('EGTGauge_ref')

    e = e + 10
    if e > 100 then e = 100 end

    ipc.writeLvar('EGTGauge_ref', e)
    A172_EGTref_show ()

end

function A172_EGTref_show ()

    DspShow('EGT', 'Ref', ' EGTref', '  ' .. ipc.readLvar('EGTGauge_ref') .. '%')

end



-- ## Cabin temperature

function A172_CabinHeat_inc ()

	local t = ipc.readLvar("CabinTempControl")
    t = t + 5
    if t > 100 then t = 100 end
	ipc.writeLvar("CabinTempControl", t)
	A172_CabinHeat_show ()

end

function A172_CabinHeat_incfast ()

	local t = ipc.readLvar("CabinTempControl")
    t = t + 10
    if t > 100 then t = 100 end
	ipc.writeLvar("CabinTempControl", t)
	A172_CabinHeat_show ()

end


function A172_CabinHeat_dec ()

	local t = ipc.readLvar("CabinTempControl")
    t = t - 5
    if t < 0 then t = 0 end
	ipc.writeLvar("CabinTempControl", t)
	A172_CabinHeat_show ()

end

function A172_CabinHeat_decfast ()

	local t = ipc.readLvar("CabinTempControl")
    t = t - 10
    if t < 0 then t = 0 end
	ipc.writeLvar("CabinTempControl", t)
	A172_CabinHeat_show ()

end


function A172_CabinHeat_show ()

    local t = ipc.readLvar("CabinTempControl")
	DspShow("HEAT", t .. '%', 'Cab.Heat', '  ' .. t .. '%')

end

function A172_CabinVent_inc ()

	local t = ipc.readLvar("CabinVent")
    t = t + 5
    if t > 100 then t = 100 end
	ipc.writeLvar("CabinVent", t)
	A172_CabinVent_show ()

end

function A172_CabinVent_incfast ()

	local t = ipc.readLvar("CabinVent")
    t = t + 10
    if t > 100 then t = 100 end
	ipc.writeLvar("CabinVent", t)
	A172_CabinVent_show ()

end


function A172_CabinVent_dec ()

	local t = ipc.readLvar("CabinVent")
    t = t - 5
    if t < 0 then t = 0 end
	ipc.writeLvar("CabinVent", t)
	A172_CabinVent_show ()

end

function A172_CabinVent_decfast ()

	local t = ipc.readLvar("CabinVent")
    t = t - 10
    if t < 0 then t = 0 end
	ipc.writeLvar("CabinVent", t)
	A172_CabinVent_show ()

end


function A172_CabinVent_show ()

    local t = ipc.readLvar("CabinVent")
	DspShow("VENT", t .. '%', 'Cab.Vent', '  ' .. t .. '%')

end


-- ## System functions ##

-- Override default ADF Select function
function A172_ADF_select_x ()

    if RADIOS_MODE ~= 3 then

        RADIOS_SUBMODE = 2

    end

    RADIOS_MODE = 3 -- ADF

    if RADIOS_SUBMODE == 2 then

    	ipc.sleep(50)
        com.write(dev, "adfxxxxx", 8)

        Default_ADF_1_init (true)
        RADIOS_SUBMODE = 1

        if ipc.readLvar("AdfBfoButton") == 0 then
            A172_ADFbuttonBfo_off ()
        else
            A172_ADFbuttonBfo_on ()
        end

    else

        A172_ADFbuttonBfo_toggle ()

    end

   	ipc.sleep(50)

end


function Timer ()

      AdditionalInfo ()

end

function AdditionalInfo ()

    local cab

    if temp_units == 0 then
        -- C
        cab = round(ipc.readLvar("CabinTemp")) .. 'C'
    else
        -- F
        cab = round(ipc.readLvar("CabinTemp")*1.8 + 32) .. 'F'
    end

    -- leading spaces
    while string.len(cab) < 4 do cab = " " .. cab end

    local flaps = ipc.readLvar("LandFlapsPos")
    local ias = math.floor(ipc.readUW("02BC")/128)
    local overspeed = ''
    local ap = 'ap'
    local tx = ipc.readLvar("xpdr_onoff_knob_pos")

    -- autopilot state
    if ipc.readUD(0x07BC) == 1 then
        ap = 'AP'
    end

    -- transponder state
    if tx == 0 then
        tx = 'tx'
    elseif tx == 1 then
        tx = 'ts'
    elseif tx == 2 then
        tx = 'tt'
    elseif tx == 3 then
        tx = 'TX'
    elseif tx == 4 then
        tx = 'TA'
    end

    -- Flap restrictions
    if ias > 110 and flaps > 0 then
        overspeed = '!'
    end

    if ias > 85 and flaps > 1 then
        overspeed = '!'
    end

    if _MCP2() then
        FLIGHT_INFO1 = ' ' .. ap .. DspStr(cab)
        FLIGHT_INFO2 = ' ' .. tx .. " F:" .. flaps .. overspeed
    else
        FLIGHT_INFO1 = "F:".. flaps .. overspeed
        -- second line is left for AP indication
    end

    DisplayResync ()

end

-- Initial info on MCP display
function InitDsp (quiet)

    QUIET = true

    -- restore fuel cut-off indication
    if ipc.readLvar('Eng1_FuelCutOffSwitch') == 1 then
        A172_FuelCutoff_open ()
    else
        A172_FuelCutoff_close ()
    end

    -- restore static air indication
    if ipc.readLvar('StaticAir') == 1 then
        A172_AltStaticAir_on ()
    else
        A172_AltStaticAir_off ()
    end

    QUIET = false

    DspCRS(CRS1)
    DspHDG(HDG)
    DspALT(ipc.readLvar("AutopilotAltAlerter")/100)
    DspVVS(VVS)

end



function InitVars ()

    -- uncomment to disable display
    AutopilotDisplayBlocked ()

    AutoDisplay = false -- disable default AP knob functions
    SPD_CRS_replace = true -- replase SPD indication with CRS indication for bush planes

    -- disable auto-restore for EFIS modes
    ipc.set("EFISrestore", 0)

    temp_units = 0 -- 0 - C, 1 - F

    if RADIOS ~= nil then -- prevent error on joysticks-only setup
        RADIOS["ADF1 Swap"]	=  A172_ADFbuttonFrq_click
        RADIOS["ADF ."]	=  A172_ADF_select_x
    end

    -- open_door_when_onground ()

end


