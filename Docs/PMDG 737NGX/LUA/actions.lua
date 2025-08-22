-- PMDG NGX737 module v4.11
-- Updated for LINDA 3.x.x
-- Oct 2017

----------------------------------------------------------------
PMDGBaseVar = 	0x00011000		-- 69632
PMDG_ClkL = 	0x20000000		-- 536870912
PMDG_ClkR = 	0x80000000		-- -2147483648
PMDG_dec = 	    0x00002000 		-- 8192
PMDG_inc = 	    0x00004000 		-- 16384
PMDG_RelL = 	0x00020000		-- 131072
PMDG_RelM = 	0x00040000		-- 262144
PMDG_RelR = 	0x00080000		-- 524288

-- ## System functions ##

function NGX_AP_SPD_show ()
    ngx_spd = ipc.readLvar("L:ngx_SPDwindow")
    if ngx_spd == nil then
        ngx_spd = 100
    end
    if ngx_spd >= 0 then
        DspSPD(ngx_spd)
    else
        DspSPDs('   ')   -- blank spd window
    end
end

function NGX_AP_ALT_show ()
    ngx_alt = (ipc.readLvar("L:ngx_ALTwindow"))
    if ngx_alt == nil then
        ngx_alt = 10000
    else
        ngx_alt = ngx_alt/100
    end
    DspALT(ngx_alt)
end

function NGX_AP_VS_show ()
    local vvs
    ipc.sleep(50)
    ngx_vvs = ipc.readLvar("L:ngx_VSwindow")
    if ngx_vvs == nil or ngx_vvs <= -20000 then
        ngx_vvs = "0000"
        vvs = 0
    else
        vvs = ngx_vvs
    end

    if _MCP1()  then
        local val = tostring(math.abs(vvs / 10))
        while string.len(val) < 3 do val = '0' .. val end
        if vvs == 0 then
            val = ' 00 '
        elseif vvs > 0 then
            val = '+' .. val
        else
            val = '-' .. val
        end
        DspShow (" VS ", val)
    else
        DspVVSs(ngx_vvs)
    end
end

function InitVars ()

    ngx_spd = 160 -- V2 B738
    ngx_hdg = 131 -- RUNWAY 13 SABE
    ngx_alt = 3000 -- CAMBIO NIVEL SABE
    ngx_vvs = 0

    ipc.set("FO_CaptToggle", 0)  -- set to 1 start with Capt enabled
    FLTALTvar = 100
    LNDALTvar = 150
    
	mcp_crs_mode = 1
    mcp_hdg_mode = 1
    -- uncomment to disable display
    -- AutopilotDisplayBlocked ()

    AutoDisplay = false

    ngx_fd_state = -1
    ngx_at_state = -1

    ngx_flaps = ''
    ngx_flaps_cur = 0
    ngx_flaps_prev = -1
    ngx_flaps_sw_cur = 0
    ngx_flaps_sw_prev = -1

    ngx_gears = ''
    ngx_gears_cur = 0
    ngx_gears_prev = -1

    ngx_cmda = '_'
    ngx_cmdb = '_'
    ngx_cwsa = '_'
    ngx_cwsb = '_'

    ngx_vor = '_'
    ngx_app = '_'

    ngx_lnav = '_'
    ngx_vnav = '_'

    -- AP states
    ngx_MCP_Speed = 0
    ngx_MCP_N1 = 0
    ngx_MCP_LvlChg = 0

    ngx_MCP_VNav = 0
    ngx_MCP_LNav = 0

    ngx_MCP_HdgSel = 0
    ngx_MCP_AltHold = 0

    ngx_MCP_CMDA = 0
    ngx_MCP_CMDB = 0
    ngx_MCP_CWSA = 0
    ngx_MCP_CWSB = 0

    ngx_MCP_VORLock = 0
    ngx_MCP_App = 0

    ngx_MCP_VS = 0

    OBS1 = ipc.readLvar("L:ngx_CRSwindowL")
    OBS2 = ipc.readLvar("L:ngx_CRSwindowR")
    OnGround = ipc.readUW("0366")
    flaps_sw = ipc.readLvar('ngx_switch_714_a')
end

function InitDsp ()

    --NGX_AP_SPD_show ()
    --NGX_AP_HDG_show ()
    --NGX_AP_ALT_show ()
    --NGX_AP_VS_show ()
end


function Timer ()

    -- FD
    if ipc.readLvar('ngx_switch_378_a') ~= ngx_fd_state then
        ngx_fd_state = ipc.readLvar('ngx_switch_378_a')
        if ngx_fd_state == 100 then
            DspFD(0)
        else
            DspFD(1)
        end
    end
    -- AT
    if ipc.readLvar('ngx_switch_380_a') ~= ngx_at_state then
        ngx_at_state = ipc.readLvar('ngx_switch_380_a')
        if ngx_at_state == 100 then
            DspAT(0)
        else
            DspAT(1)
        end
    end

    if OnGround == 1 and flaps_sw == 0 then
        NGX_FLIGHT_INFO ()
    elseif ipc.get("DSPmode") == 1 then
        -- show autopilot info
        NGX_AP_INFO ()
    else
        -- show flaps/gears info
        NGX_FLIGHT_INFO ()
    end
    -- RESTORE CORRECT AP VALUES --
    NGX_AP_BACK_SYNC ()
    -- AP MODES INDICATION --
    NGX_AP_MODES_UPDATE ()
end

function NGX_FLIGHT_INFO ()
    -- FLAPS positions and movement
    local flaps_state1, flaps_state2, flaps_sw
    flaps_state1 = ' '
    flaps_state2 = ' '
    ngx_flaps_cur = ipc.readLvar('NGXLeftInbdTrailFlaps')
    flaps_sw = ipc.readLvar('ngx_switch_714_a')

    if flaps_sw == 0 then
        ngx_flaps = '00'
    elseif flaps_sw == 10 then
        ngx_flaps = '01'
    elseif flaps_sw == 20 then
        ngx_flaps = '02'
    elseif flaps_sw == 30 then
        ngx_flaps = '05'
    elseif flaps_sw == 40 then
        ngx_flaps = '10'
    elseif flaps_sw == 50 then
        ngx_flaps = '15'
    elseif flaps_sw == 60 then
        ngx_flaps = '25'
    elseif flaps_sw == 70 then
        ngx_flaps = '30'
    elseif flaps_sw == 80 then
        ngx_flaps = '40'
    end

    if ngx_flaps_cur > ngx_flaps_prev then
        flaps_state1 = ' '
        flaps_state2 = '>'
    elseif ngx_flaps_cur < ngx_flaps_prev then
        flaps_state1 = '<'
        flaps_state2 = ' '
    end
    ngx_flaps_prev = ipc.readLvar('NGXLeftInbdTrailFlaps')

    -- GEARS positions and movement
    local gears_state
    gears_state = ipc.readLvar('ngx_switch_455_a')
    if gears_state < 30 then
        ngx_gears = "UP"
    elseif gears_state == 30 then
        ngx_gears = "OF"
    elseif gears_state > 30 then
        ngx_gears = "DN"
    end

     -- APU Display
    OnGround = ipc.readUW("0366")
    ApuEgt = ipc.readLvar('ngx_switch_35_a')
    ApuReady = ipc.readUB("648B")

    if OnGround == 1 and flaps_sw == 0 and ApuReady == 0 then
        if _MCP1() then
            FLIGHT_INFO1 = "F:" .. ngx_flaps
            FLIGHT_INFO2 = "A:" .. ApuEgt
        else
            FLIGHT_INFO1 = ' FL APU'
            FLIGHT_INFO2 = flaps_state1 .. ngx_flaps .. flaps_state2 .. '' .. ApuEgt .. 'C'
        end
    elseif OnGround == 1 and flaps_sw == 0 and ApuReady == 1 then
        if _MCP1() then
            FLIGHT_INFO1 = "F:" .. ngx_flaps
            FLIGHT_INFO2 = "A:on"
        else
            FLIGHT_INFO1 = ' FL APU'
            FLIGHT_INFO2 = flaps_state1 .. ngx_flaps .. flaps_state2 .. 'RDY'
        end
    else -- if OnGround == 0 then
        if _MCP1() then
            FLIGHT_INFO1 = "F:" .. ngx_flaps
            FLIGHT_INFO2 = "G:" .. ngx_gears
        else
            FLIGHT_INFO1 = ' FL  GR'
            FLIGHT_INFO2 = flaps_state1 .. ngx_flaps .. flaps_state2 .. ' ' .. ngx_gears
        end
    end
end

function NGX_AP_INFO ()
    -- FLAPS positions and movement
    local flaps_state1, flaps_state2, flaps_sw
    flaps_state1 = ':'
    flaps_state2 = ''
    ngx_flaps_cur = ipc.readLvar('NGXLeftInbdTrailFlaps')
    flaps_sw = ipc.readLvar('ngx_switch_714_a')

    if ngx_flaps_cur ~= ngx_flaps_prev or ngx_flaps_sw_cur ~= ngx_flaps_sw_prev then
        if flaps_sw == 0 then
            ngx_flaps = '00'
        elseif flaps_sw == 10 then
            ngx_flaps = '01'
        elseif flaps_sw == 20 then
            ngx_flaps = '02'
        elseif flaps_sw == 30 then
            ngx_flaps = '05'
        elseif flaps_sw == 40 then
            ngx_flaps = '10'
        elseif flaps_sw == 50 then
            ngx_flaps = '15'
        elseif flaps_sw == 60 then
            ngx_flaps = '25'
        elseif flaps_sw == 70 then
            ngx_flaps = '30'
        elseif flaps_sw == 80 then
            ngx_flaps = '40'
        end

        -- determine flap direction of travel
        if _MCP1() then
            if ngx_flaps_cur > ngx_flaps_prev then
                flaps_state1 = ' '
                flaps_state2 = '>'
            elseif ngx_flaps_cur < ngx_flaps_prev then
                flaps_state1 = '<'
                flaps_state2 = ' '
            end
            ngx_flaps_txt = 'F' .. flaps_state1 .. ngx_flaps
            Dsp0(ngx_flaps_txt)
        elseif _MCP2() then
            if ngx_flaps_cur > ngx_flaps_prev then
                flaps_state1 = ' '
                flaps_state2 = '>'
            elseif ngx_flaps_cur < ngx_flaps_prev then
                flaps_state1 = '<'
                flaps_state2 = ' '
            end
            ngx_flaps_txt = 'F:' .. flaps_state1 .. ngx_flaps .. flaps_state2
            DspMed1(ngx_flaps_txt)
        else -- MCP2a default
            if ngx_flaps_cur > ngx_flaps_prev then
                flaps_state1 = '>'
            elseif ngx_flaps_cur < ngx_flaps_prev then
                flaps_state1 = '<'
            end
            ngx_flaps_txt = 'F:' .. flaps_state1 .. ngx_flaps
            if Airbus then
                DspShow(ngx_flaps_txt)
            else
                DspMed1(ngx_flaps_txt)
            end
        end
        ngx_flaps_sw_prev = ngx_flaps_sw_cur
        ngx_flaps_prev = ipc.readLvar('NGXLeftInbdTrailFlaps')
    end

    -- GEARS positions and movement
    ngx_gears_cur = ipc.readLvar('ngx_switch_455_a')
    ngx_gears_txt = 'GEAR'
    if ngx_gears_cur ~= ngx_gears_prev then
        if ngx_gears_cur < 30 then
            if _MCP1() then
                ngx_gears_txt = 'G:UP'
            else
                ngx_gears_txt = 'Gear:UP'
            end
        elseif ngx_gears_cur == 30 then
            if _MCP1() then
                ngx_gears_txt = 'G:OF'
            else
                ngx_gears_txt = 'Gear:OF'
            end
        elseif ngx_gears_cur > 30 then
            if _MCP1() then
                ngx_gears_txt = 'G:DN'
            else
                ngx_gears_txt = 'Gear:DN'
            end
        end
        -- display flap position
        if _MCP1() then
            Dsp1(ngx_gears_txt)
        elseif _MCP2() and (_MCP2a() and not Airbus) then
            DspMed2(ngx_gears_txt)
        else
            DspShow(ngx_gears_txt)
        end
        ngx_gears_prev = ngx_gears_cur
    end

    -- CMD A
    if ngx_MCP_CMDA ~= ipc.readLvar("L:ngx_MCP_CMDA") then
        ngx_MCP_CMDA = ipc.readLvar("L:ngx_MCP_CMDA")
        if ngx_MCP_CMDA == 1 then
            ngx_cmda = "A"
        else
            ngx_cmda = "_"
        end
    end
    if ngx_MCP_CMDB ~= ipc.readLvar("L:ngx_MCP_CMDB") then
        ngx_MCP_CMDB = ipc.readLvar("L:ngx_MCP_CMDB")
        if ngx_MCP_CMDB == 1 then
            ngx_cmdb = "B"
        else
            ngx_cmdb = "_"
        end
    end
    if ngx_MCP_CWSA ~= ipc.readLvar("L:ngx_MCP_CWSA") then
        ngx_MCP_CWSA = ipc.readLvar("L:ngx_MCP_CWSA")
        if ngx_MCP_CWSA == 1 then
            ngx_cwsa = "A"
        else
            ngx_cwsa = "_"
        end
    end
    if ngx_MCP_CWSB ~= ipc.readLvar("L:ngx_MCP_CWSB") then
        ngx_MCP_CWSB = ipc.readLvar("L:ngx_MCP_CWSB")
        if ngx_MCP_CWSB == 1 then
            ngx_cwsb = "B"
        else
            ngx_cwsb = "_"
        end
    end

    FLIGHT_INFO1 = ' AP:' .. ngx_cmda .. ngx_cmdb
    FLIGHT_INFO2 = 'CWS:' .. ngx_cwsa .. ngx_cwsb

    -- LOC / APP modes indication
    if ngx_MCP_VORLock ~= ipc.readLvar("L:ngx_MCP_VORLock") then
        ngx_MCP_VORLock = ipc.readLvar("L:ngx_MCP_VORLock")
        if ngx_MCP_VORLock == 1 then
            ngx_vor = "V"
        else
            ngx_vor = "_"
        end
    end
    if ngx_MCP_App ~= ipc.readLvar("L:ngx_MCP_App") then
        ngx_MCP_App = ipc.readLvar("L:ngx_MCP_App")
        if ngx_MCP_App == 1 then
            ngx_app = "A"
        else
            ngx_app = "_"
        end
    end

    if _MCP1()  then
        FLIGHT_INFO1 = ngx_cmda .. ngx_cmdb .. ' ' .. ngx_lnav
        FLIGHT_INFO2 = ngx_cwsa .. ngx_cwsa .. ' ' .. ngx_vnav
    elseif _MCP2() then
        FLIGHT_INFO1 = FLIGHT_INFO1 .. ' ' .. ngx_app
        FLIGHT_INFO2 = FLIGHT_INFO2 .. ' ' .. ngx_vor
    else -- MCP2a
        FLIGHT_INFO1 = FLIGHT_INFO1 .. ngx_app
        FLIGHT_INFO2 = FLIGHT_INFO2 .. ngx_vor
    end
end

function NGX_AP_BACK_SYNC ()
    if ngx_spd ~= ipc.readLvar("L:ngx_SPDwindow") then
        NGX_AP_SPD_show ();
    end
    if ngx_hdg ~= ipc.readLvar("L:ngx_HDGwindow") then
        NGX_AP_HDG_show ();
    end
    if ngx_alt ~= ipc.readLvar("L:ngx_ALTwindow") then
        NGX_AP_ALT_show ();
    end
    if ngx_vvs ~= ipc.readLvar("L:ngx_VSwindow") then
        NGX_AP_VS_show ();
    end
end

function NGX_AP_MODES_UPDATE ()
    if _MCP1()  then return end
    if (ipc.readLvar("L:ngx_MCP_Speed") ~= ngx_MCP_Speed
       or ipc.readLvar("L:ngx_MCP_N1") ~= ngx_MCP_N1)
       and not (ipc.readLvar("L:ngx_MCP_LvlChg") == 1)  then
        ngx_MCP_Speed = ipc.readLvar("L:ngx_MCP_Speed")
        ngx_MCP_N1 = ipc.readLvar("L:ngx_MCP_N1")
        if ngx_MCP_N1 == 1 then
            DspSPD_N1_on ()
        else
            DspSPD_N1_off ()
            if ngx_MCP_Speed == 1 then
                DspSPD_AP_on ()
            else
                DspSPD_AP_off ()
            end
        end
    end

    -- LVL CHG
    if ipc.readLvar("L:ngx_MCP_LvlChg") ~= ngx_MCP_LvlChg then
        ngx_MCP_LvlChg = ipc.readLvar("L:ngx_MCP_LvlChg")
        ngx_MCP_Speed = ipc.readLvar("L:ngx_MCP_Speed")
        ngx_MCP_N1 = ipc.readLvar("L:ngx_MCP_N1")
        if ngx_MCP_LvlChg == 1 then
            DspSPD_FLCH_on ()
        else
            DspSPD_FLCH_off ()
            if ngx_MCP_N1 == 1 then
                DspSPD_N1_on ()
            else
                if ngx_MCP_Speed == 1 then
                    DspSPD_AP_on ()
                else
                    DspSPD_AP_off ()
                end
            end
        end
    end

    -- LNAV
    if ipc.readLvar("L:ngx_MCP_LNav") ~= ngx_MCP_LNav then
        ngx_MCP_LNav = ipc.readLvar("L:ngx_MCP_LNav")
        ngx_MCP_HdgSel = ipc.readLvar("L:ngx_MCP_HdgSel")
        if ngx_MCP_LNav == 1 then
            DspLNAV_on ()
            ngx_lnav = 'L'
        else
            ngx_lnav = '_'
            DspLNAV_off ()
        end

        if ngx_MCP_HdgSel == 1 then
            DspHDG_AP_on ()
        else
            DspHDG_AP_off ()
        end
    else
        -- HDG
        if ipc.readLvar("L:ngx_MCP_HdgSel") ~= ngx_MCP_HdgSel then
            ngx_MCP_HdgSel = ipc.readLvar("L:ngx_MCP_HdgSel")
            if ngx_MCP_HdgSel == 1 then
                DspHDG_AP_on ()
            else
                DspHDG_AP_off ()
            end
        end
    end

    -- VNAV
    if ipc.readLvar("L:ngx_MCP_VNav") ~= ngx_MCP_VNav then
        ngx_MCP_VNav = ipc.readLvar("L:ngx_MCP_VNav")
        ngx_MCP_AltHold = ipc.readLvar("L:ngx_MCP_AltHold")

        if ngx_MCP_VNav == 1 then
            DspVNAV_on ()
            ngx_vnav = 'V'
        else
            ngx_vnav = '_'
            DspVNAV_off ()
            if ngx_MCP_AltHold == 1 then
                DspALT_AP_on ()
            else
                DspALT_AP_off ()
            end
        end
    else
        -- ALT
        if ipc.readLvar("L:ngx_MCP_AltHold") ~= ngx_MCP_AltHold then
            ngx_MCP_AltHold = ipc.readLvar("L:ngx_MCP_AltHold")

            if ngx_MCP_AltHold == 1 then
                DspALT_AP_on ()
            else
                DspALT_AP_off ()
            end
        end
    end

    -- VS
    if ipc.readLvar("L:ngx_MCP_VS") ~= ngx_MCP_VS then
        ngx_MCP_VS = ipc.readLvar("L:ngx_MCP_VS")
        if ngx_MCP_VS == 1 then
            DspVVS_AP_on ()
        else
            DspVVS_AP_off ()
        end
    end
end

function DspMode_Toggle()
    _log('DspMode_Toggle')
    DSP_MODE_toggle()
end

-- ## EFIS controls ###############
function EFIS_FO_CaptToggle ()
    FO_CaptToggleVar = ipc.get("FO_CaptToggle")
    if FO_CaptToggleVar == 1 then
        ipc.set("FO_CaptToggle", 0)
        DspShow ("EFIS", "Capt")
    else
        ipc.set("FO_CaptToggle", 1)
        DspShow ("EFIS", "FO")
    end
end

-- $$ Minimums

function NGX_EFIS_MINS_inc ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_MINS_inc ()
    else
        ipc.control(PMDGBaseVar+355, PMDG_inc)
        DspShow ("MINS", "inc")
    end
end

function NGX_EFIS_MINS_dec ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_MINS_dec ()
    else
        ipc.control(PMDGBaseVar+355, PMDG_dec)
        DspShow ("MINS", "dec")
    end
end

function NGX_EFIS_MINS_MODE_baro ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_MODE_baro ()
    else
        if ipc.readLvar("ngx_switch_356_a") == 0 then
            ipc.control(PMDGBaseVar+356, PMDG_ClkL)
        end
        DspShow ("MINS", "baro")
     end
end

function NGX_EFIS_MINS_MODE_radio ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_MINS_MODE_radio ()
    else
        if ipc.readLvar("ngx_switch_356_a") == 100 then
            ipc.control(PMDGBaseVar+356, PMDG_ClkR)
        end
        DspShow ("MINS", "rad")
    end
end

function NGX_EFIS_MINS_MODE_toggle ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_MINS_MODE_toggle ()
    else
        if _t("BaroRadio") then
            NGX_EFIS_MINS_MODE_baro ()
        else
            NGX_EFIS_MINS_MODE_radio ()
        end
    end
end

function NGX_EFIS_MINS_RST ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_MINS_RST ()
    else
        ipc.control(PMDGBaseVar+357, PMDG_ClkL)
        DspShow ("MINS", "RST")
    end
end

-- $$ Barometer

function NGX_EFIS_BARO_inc ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_BARO_inc ()
    else
        ipc.control(PMDGBaseVar+365, PMDG_inc)
        DspShow ("BARO", "inc")
    end
end

function NGX_EFIS_BARO_dec ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_BARO_dec ()
    else
        ipc.control(PMDGBaseVar+365, PMDG_dec)
        DspShow ("BARO", "dec")
    end
end

function NGX_EFIS_BARO_MODE_hpa ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_BARO_MODE_hpa ()
    else
        if ipc.readLvar('ngx_switch_366_a') == 0 then
            ipc.control(PMDGBaseVar+366, PMDG_ClkL)
            DspShow ("BARO", "HPa")
        end
    end
end

function NGX_EFIS_BARO_MODE_inHg ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_BARO_MODE_inHg ()
    else
        if ipc.readLvar('ngx_switch_366_a') == 100 then
            ipc.control(PMDGBaseVar+366, PMDG_ClkR)
            DspShow ("BARO", "inHg")
        end
    end
end

function NGX_EFIS_BARO_MODE_toggle ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_BARO_MODE_toggle ()
    else
        if _t("InHPA") then
            NGX_EFIS_BARO_MODE_hpa ()
        else
            NGX_EFIS_BARO_MODE_inHg ()
        end
    end
end

function NGX_EFIS_BARO_STD_toggle ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_BARO_STD_toggle ()
    else
    	ipc.control(PMDGBaseVar+367, PMDG_ClkL)
    	DspShow ("BARO", "STD")
    end
end


-- $$ Navigation Display Mode

function NGX_EFIS_ND_MODE_show ()
local txt
local pos = ipc.readLvar('ngx_switch_359_a')
    if pos == 0 then txt = "APP"
    elseif pos == 10 then txt = "VOR"
    elseif pos == 20 then txt = "MAP"
    elseif pos == 30 then txt = "PLN"
    end
    DspShow ("ND", txt)
end

function NGX_EFIS_ND_MODE_APP ()
    NGX_EFIS_ND_MODE_move (0)
end

function NGX_EFIS_ND_MODE_VOR ()
    NGX_EFIS_ND_MODE_move (10)
end

function NGX_EFIS_ND_MODE_MAP ()
    NGX_EFIS_ND_MODE_move (20)
end

function NGX_EFIS_ND_MODE_PLN ()
    NGX_EFIS_ND_MODE_move (30)
end

function NGX_EFIS_ND_MODE_move (val)
local pos = ipc.readLvar('ngx_switch_359_a')
    if pos > val then
        while pos > val do
            NGX_EFIS_ND_MODE_dec()
            pos = ipc.readLvar('ngx_switch_359_a')
        end
    else
        while pos < val do
            NGX_EFIS_ND_MODE_inc()
            pos = ipc.readLvar('ngx_switch_359_a')
        end
    end
end

function NGX_EFIS_ND_MODE_inc ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_ND_MODE_inc ()
    else
        ipc.control(PMDGBaseVar+359, PMDG_inc)
        NGX_EFIS_ND_MODE_show ()
    end
end

function NGX_EFIS_ND_MODE_dec ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_ND_MODE_dec ()
    else
        ipc.control(PMDGBaseVar+359, PMDG_dec)
        NGX_EFIS_ND_MODE_show ()
    end
end

function NGX_EFIS_ND_MODE_CTR ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_ND_MODE_CTR ()
    else
        ipc.control(PMDGBaseVar+360, PMDG_ClkL)
        DspShow ("ND", "CTR")
    end
end

-- $$ Navigation Display Range

function NGX_EFIS_ND_RNG_show ()
local txt
local pos = ipc.readLvar('ngx_switch_361_a')
    if pos == 0 then txt = "5"
    elseif pos == 10 then txt = "10"
    elseif pos == 20 then txt = "20"
    elseif pos == 30 then txt = "40"
    elseif pos == 40 then txt = "80"
    elseif pos == 50 then txt = "160"
    elseif pos == 60 then txt = "320"
    elseif pos == 70 then txt = "640"
    end
    DspShow ("RNG", txt)
end

function NGX_EFIS_ND_RNG_5 ()
    NGX_EFIS_ND_RNG_move (0)
end

function NGX_EFIS_ND_RNG_10 ()
    NGX_EFIS_ND_RNG_move (10)
end

function NGX_EFIS_ND_RNG_20 ()
    NGX_EFIS_ND_RNG_move (20)
end

function NGX_EFIS_ND_RNG_40 ()
    NGX_EFIS_ND_RNG_move (30)
end

function NGX_EFIS_ND_RNG_80 ()
    NGX_EFIS_ND_RNG_move (40)
end

function NGX_EFIS_ND_RNG_160 ()
    NGX_EFIS_ND_RNG_move (50)
end

function NGX_EFIS_ND_RNG_320 ()
    NGX_EFIS_ND_RNG_move (60)
end

function NGX_EFIS_ND_RNG_640 ()
    NGX_EFIS_ND_RNG_move (70)
end

function NGX_EFIS_ND_RNG_move (val)
local pos = ipc.readLvar('ngx_switch_361_a')
    if pos > val then
        while pos > val do
            NGX_EFIS_ND_RNG_dec()
            pos = ipc.readLvar('ngx_switch_361_a')
        end
    else
        while pos < val do
            NGX_EFIS_ND_RNG_inc()
            pos = ipc.readLvar('ngx_switch_361_a')
        end
    end
end

function NGX_EFIS_ND_RNG_inc ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_ND_RNG_inc ()
    else
    	ipc.control(PMDGBaseVar+361, PMDG_inc)
    	NGX_EFIS_ND_RNG_show ()
    end
end

function NGX_EFIS_ND_RNG_dec ()
    if ipc.get("FO_CaptToggle") == 1 then
	    NGX_EFIS_FO_ND_RNG_dec ()
    else
    	ipc.control(PMDGBaseVar+361, PMDG_dec)
    	NGX_EFIS_ND_RNG_show ()
    end
end


function NGX_EFIS_ND_RNG_TFC ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_ND_RNG_TFC ()
    else
    	ipc.control(PMDGBaseVar+362, PMDG_ClkL)

	NGX_TOGGLE_STBY_TARA()

    	DspShow ("RNG", "TFC")
    end
end

-- $$ EFIS Buttons/Switches

function NGX_EFIS_FPV ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_FPV ()
    else
    	ipc.control(PMDGBaseVar+363, PMDG_ClkL)
    	DspShow ("EFIS", "FPV")
    end
end

function NGX_EFIS_MTRS ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_MTRS ()
    else
    	ipc.control(PMDGBaseVar+364, PMDG_ClkL)
    	DspShow ("EFIS", "MTRS")
    end
end


function NGX_EFIS_NAV1_inc ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_NAV1_inc ()
    else
        ipc.control(PMDGBaseVar+358, PMDG_ClkR)
    end
end

function NGX_EFIS_NAV1_dec ()
    if ipc.get("FO_CaptToggle") == 1 then
        NGX_EFIS_FO_NAV1_dec ()
    else
        ipc.control(PMDGBaseVar+358, PMDG_ClkL)
    end
end

function NGX_EFIS_NAV1_vor ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_NAV1_vor ()
    else
        NGX_EFIS_NAV1_inc ()
        NGX_EFIS_NAV1_inc ()
        DspShow ("EFIS", "VOR1")
    end
end

function NGX_EFIS_NAV1_adf ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_NAV1_adf ()
    else
    	NGX_EFIS_NAV1_dec ()
    	NGX_EFIS_NAV1_dec ()
    	DspShow ("EFIS", "ADF1")
    end
end

function NGX_EFIS_NAV1_off ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_NAV1_off ()
    else
    	NGX_EFIS_NAV1_inc ()
    	NGX_EFIS_NAV1_inc ()
    	NGX_EFIS_NAV1_dec ()
    	DspShow ("EFIS", "OFF1")
    end
end

function NGX_EFIS_NAV1_toggle ()
local pos = ipc.readLvar('ngx_switch_358_a')
    if pos == 0 then
    	NGX_EFIS_NAV1_off ()
    elseif pos == 50 then
    	NGX_EFIS_NAV1_adf ()
    elseif pos == 100 then
    	NGX_EFIS_NAV1_vor ()
    end
end

function NGX_EFIS_NAV2_inc ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_NAV2_inc ()
    else
    	ipc.control(PMDGBaseVar+368, PMDG_ClkR)
    end
end

function NGX_EFIS_NAV2_dec ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_NAV2_dec ()
    else
    	ipc.control(PMDGBaseVar+368, PMDG_ClkL)
    end
end

function NGX_EFIS_NAV2_vor ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_NAV2_vor ()
    else
    	NGX_EFIS_NAV2_inc ()
    	NGX_EFIS_NAV2_inc ()
    	DspShow ("EFIS", "VOR2")
    end
end

function NGX_EFIS_NAV2_adf ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_NAV2_adf ()
    else
    	NGX_EFIS_NAV2_dec ()
    	NGX_EFIS_NAV2_dec ()
    	DspShow ("INOP", "ADF2")
    end
end

function NGX_EFIS_NAV2_off ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_NAV2_off ()
    else
    	NGX_EFIS_NAV2_inc ()
    	NGX_EFIS_NAV2_inc ()
    	NGX_EFIS_NAV2_dec ()
    DspShow ("EFIS", "OFF2")
    end
end

function NGX_EFIS_NAV2_toggle ()
local pos = ipc.readLvar('ngx_switch_368_a')
    if pos == 0 then
    	NGX_EFIS_NAV2_off ()
    elseif pos == 50 then
    	NGX_EFIS_NAV2_adf ()
    elseif pos == 100 then
    	NGX_EFIS_NAV2_vor ()
    end
end

function NGX_EFIS_WXR ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_WXR ()
    else
    	ipc.control(PMDGBaseVar+369, PMDG_ClkL)
    	DspShow ("EFIS", "WXR")
    end
end

function NGX_EFIS_STA ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_STA ()
    else
    	ipc.control(PMDGBaseVar+370, PMDG_ClkL)
    	DspShow ("EFIS", "STA")
    end
end

function NGX_EFIS_WPT ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_WPT ()
    else
    	ipc.control(PMDGBaseVar+371, PMDG_ClkL)
    	DspShow ("EFIS", "WPT")
    end
end

function NGX_EFIS_ARPT ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_ARPT ()
    else
    	ipc.control(PMDGBaseVar+372, PMDG_ClkL)
    	DspShow ("EFIS", "ARPT")
    end
end

function NGX_EFIS_DATA ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_DATA ()
    else
    	ipc.control(PMDGBaseVar+373, PMDG_ClkL)
    	DspShow ("EFIS", "DATA")
    end
end

function NGX_EFIS_POS ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_POS ()
    else
    	ipc.control(PMDGBaseVar+374, PMDG_ClkL)
    	DspShow ("EFIS", "POS")
    end
end

function NGX_EFIS_TERR ()
    if ipc.get("FO_CaptToggle") == 1 then
    	NGX_EFIS_FO_TERR ()
    else
    	ipc.control(PMDGBaseVar+375, PMDG_ClkL)
    	DspShow ("EFIS", "TERR")
    end
end

-- ## EFIS FO controls ###############

function EFIS_FO_CaptToggle ()
    ipc.sleep(50)
    FO_CaptToggleVar = ipc.get("FO_CaptToggle")
    if FO_CaptToggleVar == 1 then
    	ipc.set("FO_CaptToggle", 0)
    	DspShow ("EFIS", "Capt")
    else
    	ipc.set("FO_CaptToggle", 1)
    	DspShow ("EFIS", "FO")
    end
end

-- $$ Minimums FO

function NGX_EFIS_FO_MINS_inc ()
    ipc.control(PMDGBaseVar+411, PMDG_inc)
    DspShow ("MINS", "inc")
end

function NGX_EFIS_FO_MINS_dec ()
    ipc.control(PMDGBaseVar+411, PMDG_dec)
    DspShow ("MINS", "dec")
end

function NGX_EFIS_FO_MINS_MODE_baro ()
    if ipc.readLvar("ngx_switch_412_a") == 0 then
        ipc.control(PMDGBaseVar+412, PMDG_ClkL)
	    DspShow ("MINS", "BARO")
    end
end

function NGX_EFIS_FO_MINS_MODE_radio ()
    if ipc.readLvar("ngx_switch_412_a") == 100 then
        ipc.control(PMDGBaseVar+412, PMDG_ClkR)
	    DspShow ("MINS", "RAD")
    end
end

function NGX_EFIS_FO_MINS_MODE_toggle ()
    if _t("BaroRadio") then
        NGX_EFIS_FO_MINS_MODE_baro ()
    else
        NGX_EFIS_FO_MINS_MODE_radio ()
    end
end

function NGX_EFIS_FO_MINS_RST ()
    ipc.control(PMDGBaseVar+413, PMDG_ClkL)
    DspShow ("MINS", "RST")
end

-- $$ Barometer FO

function NGX_EFIS_FO_BARO_inc ()
    ipc.control(PMDGBaseVar+421, PMDG_inc)
    DspShow ("BARO", "inc")
end

function NGX_EFIS_FO_BARO_dec ()
    ipc.control(PMDGBaseVar+421, PMDG_dec)
    DspShow ("BARO", "dec")
end

function NGX_EFIS_FO_BARO_MODE_hpa ()
    if ipc.readLvar('ngx_switch_422_a') == 0 then
	    ipc.control(PMDGBaseVar+422, PMDG_ClkL)
	    DspShow ("BARO", "HPA")
    end
end

function NGX_EFIS_FO_BARO_MODE_inHg ()
    if ipc.readLvar('ngx_switch_422_a') == 100 then
	    ipc.control(PMDGBaseVar+422, PMDG_ClkL)
    	DspShow ("BARO", "inHg")
    end
end

function NGX_EFIS_FO_BARO_MODE_toggle ()
    if _t("InHPA") then
	    NGX_EFIS_FO_BARO_MODE_inHg ()
    else
        NGX_EFIS_FO_BARO_MODE_hpa ()
    end
end

function NGX_EFIS_FO_BARO_STD_toggle ()
    ipc.control(PMDGBaseVar+423, PMDG_ClkL)
    DspShow ("BARO", "STD")
end

-- $$ Navigation Display Mode FO
function NGX_EFIS_FO_ND_MODE_show ()
local txt
local pos = ipc.readLvar('ngx_switch_359_a')
    if pos == 0 then txt = "APP"
    elseif pos == 10 then txt = "VOR"
    elseif pos == 20 then txt = "MAP"
    elseif pos == 30 then txt = "PLN"
    end
    DspShow ("ND", txt)
end

function NGX_EFIS_FO_ND_MODE_inc ()
    ipc.control(PMDGBaseVar+415, PMDG_inc)
    NGX_EFIS_FO_ND_MODE_show ()
end

function NGX_EFIS_FO_ND_MODE_dec ()
    ipc.control(PMDGBaseVar+415, PMDG_dec)
    NGX_EFIS_FO_ND_MODE_show ()
end

function NGX_EFIS_FO_ND_MODE_CTR ()
    ipc.control(PMDGBaseVar+416, PMDG_ClkL)
    DspShow ("ND", "CTR")
end

-- $$ Navigation Display Range FO

function NGX_EFIS_FO_ND_RNG_show ()
local txt
local pos = ipc.readLvar('ngx_switch_415_a')
    if pos == 0 then txt ="5"
    elseif pos == 10 then txt = "10"
    elseif pos == 20 then txt = "20"
    elseif pos == 30 then txt = "40"
    elseif pos == 40 then txt = "80"
    elseif pos == 50 then txt = "160"
    elseif pos == 60 then txt = "320"
    elseif pos == 70 then txt = "640"
    end
    DspShow ("RNG", txt)
end

function NGX_EFIS_FO_ND_RNG_inc ()
    ipc.control(PMDGBaseVar+417, PMDG_inc)
    NGX_EFIS_FO_ND_RNG_show ()
end

function NGX_EFIS_FO_ND_RNG_dec ()
    ipc.control(PMDGBaseVar+417, PMDG_dec)
    NGX_EFIS_FO_ND_RNG_show ()
end

function NGX_EFIS_FO_ND_RNG_TFC ()
    ipc.control(PMDGBaseVar+418, PMDG_ClkL)
    DspShow ("RNG", "TFC")
end

-- $$ EFIS Buttons/Switches FO

function NGX_EFIS_FO_FPV ()
    ipc.control(PMDGBaseVar+419, PMDG_ClkL)
	DspShow ("EFIS", "FPV")
end

function NGX_EFIS_FO_MTRS ()
    ipc.control(PMDGBaseVar+420, PMDG_ClkL)
    DspShow ("EFIS", "MTRS")
end

function NGX_EFIS_FO_NAV1_inc ()
    ipc.control(PMDGBaseVar+414, PMDG_ClkR)
end

function NGX_EFIS_FO_NAV1_dec ()
    ipc.control(PMDGBaseVar+414, PMDG_ClkL)
end

function NGX_EFIS_FO_NAV1_vor ()
    NGX_EFIS_FO_NAV1_inc ()
    NGX_EFIS_FO_NAV1_inc ()
    DspShow ("EFIS", "VOR1")
end

function NGX_EFIS_FO_NAV1_adf ()
    NGX_EFIS_FO_NAV1_dec ()
    NGX_EFIS_FO_NAV1_dec ()
    DspShow ("EFIS", "ADF1")
end

function NGX_EFIS_FO_NAV1_off ()
    NGX_EFIS_FO_NAV1_inc ()
    NGX_EFIS_FO_NAV1_inc ()
    NGX_EFIS_FO_NAV1_dec ()
    DspShow ("EFIS", "OFF1")
end

----

function NGX_EFIS_FO_NAV2_inc ()
    ipc.control(PMDGBaseVar+424, PMDG_ClkR)
end

function NGX_EFIS_FO_NAV2_dec ()
    ipc.control(PMDGBaseVar+424, PMDG_ClkL)
end

function NGX_EFIS_FO_NAV2_vor ()
    NGX_EFIS_FO_NAV2_inc ()
    NGX_EFIS_FO_NAV2_inc ()
    DspShow ("EFIS", "VOR2")
end

function NGX_EFIS_FO_NAV2_adf ()
    NGX_EFIS_FO_NAV2_dec ()
    NGX_EFIS_FO_NAV2_dec ()
    DspShow ("EFIS", "ADF2")
end

function NGX_EFIS_FO_NAV2_off ()
    NGX_EFIS_FO_NAV2_inc ()
    NGX_EFIS_FO_NAV2_inc ()
    NGX_EFIS_FO_NAV2_dec ()
    DspShow ("EFIS", "OFF2")
end

--------------

function NGX_EFIS_FO_WXR ()
    ipc.control(PMDGBaseVar+425, PMDG_ClkL)
    DspShow ("EFIS", "WXR")
end

function NGX_EFIS_FO_STA ()
    ipc.control(PMDGBaseVar+426, PMDG_ClkL)
    DspShow ("EFIS", "STA")
end

function NGX_EFIS_FO_WPT ()
    ipc.control(PMDGBaseVar+427, PMDG_ClkL)
    DspShow ("EFIS", "WPT")
end

function NGX_EFIS_FO_ARPT ()
    ipc.control(PMDGBaseVar+428, PMDG_ClkL)
    DspShow ("EFIS", "ARPT")
end

function NGX_EFIS_FO_DATA ()
    ipc.control(PMDGBaseVar+429, PMDG_ClkL)
    DspShow ("EFIS", "DATA")
end

function NGX_EFIS_FO_POS ()
    ipc.control(PMDGBaseVar+430, PMDG_ClkL)
    DspShow ("EFIS", "POS")
end

function NGX_EFIS_FO_TERR ()
    ipc.control(PMDGBaseVar+431, PMDG_ClkL)
    DspShow ("EFIS", "TERR")
end

-- ## Autopilot switches ###############

function NGX_AP_FD1_toggle ()
    if ipc.readLvar('ngx_switch_378_a') == 100 then
        NGX_AP_FD1_on()
    else
        NGX_AP_FD1_off()
    end
end

function NGX_AP_FD1_on ()
    if ipc.readLvar('ngx_switch_378_a') ~= 0 then
        ipc.control(PMDGBaseVar+378, PMDG_ClkR)
        DspShow ("FD1", "on")
    end
end

function NGX_AP_FD1_off ()
    if ipc.readLvar('ngx_switch_378_a') ~= 100 then
        ipc.control(PMDGBaseVar+378, PMDG_ClkL)
        DspShow ("FD1", "off")
    end
end

function NGX_AP_FD2_toggle ()
    if ipc.readLvar('ngx_switch_407_a') == 100 then
        NGX_AP_FD2_on()
    else
        NGX_AP_FD2_off()
    end
end

function NGX_AP_FD2_on ()
    if ipc.readLvar('ngx_switch_407_a') ~= 0 then
        ipc.control(PMDGBaseVar+407, PMDG_ClkR)
        DspShow ("FD2", "on")
    end
end

function NGX_AP_FD2_off ()
    if ipc.readLvar('ngx_switch_407_a') ~= 100 then
        ipc.control(PMDGBaseVar+407, PMDG_ClkL)
        DspShow ("FD2", "off")
    end
end

function NGX_AP_FD_both_on ()
    if ipc.readLvar('ngx_switch_378_a') ~= 0 then
        NGX_AP_FD1_on()
    end
    if ipc.readLvar('ngx_switch_407_a') ~= 0 then
        NGX_AP_FD2_on ()
    end
    DspShow ("FDs", "on")
end

function NGX_AP_FD_both_off ()
    if ipc.readLvar('ngx_switch_378_a') ~= 100 then
        NGX_AP_FD1_off ()
    end
    if ipc.readLvar('ngx_switch_407_a') ~= 100 then
        NGX_AP_FD2_off ()
    end
    DspShow ("FDs", "off")
end

function NGX_AP_FD_both_toggle ()
    if ipc.readLvar('ngx_switch_378_a') == 0 then
        NGX_AP_FD1_toggle()
        NGX_AP_FD2_toggle()
        DspShow ("FDs", "off")
    else
        NGX_AP_FD1_toggle()
        NGX_AP_FD2_toggle()
        DspShow ("FDs", "on")
    end
end

function NGX_AP_ATHR_toggle ()
    if ipc.readLvar('ngx_switch_380_a') == 100 then
        NGX_AP_ATHR_arm ()
    else
        NGX_AP_ATHR_off ()
    end
end

function NGX_AP_ATHR_arm ()
    if ipc.readLvar('ngx_switch_380_a') == 100 then
        ipc.control(PMDGBaseVar+380, PMDG_ClkR)
	    DspShow ("ATHR", "arm")
    end
end

function NGX_AP_ATHR_off ()
    if ipc.readLvar('ngx_switch_380_a') == 0 then
        ipc.control(PMDGBaseVar+380, PMDG_ClkL)
	    DspShow ("ATHR", "off")
    end
end

function NGX_AP_MASTER_toggle ()
    if ipc.readLvar('ngx_switch_406_a') == 100 then
        NGX_AP_MASTER_on()
    else
        NGX_AP_MASTER_off()
    end
end

function NGX_AP_MASTER_on ()
    if ipc.readLvar('ngx_switch_406_a') ~= 0 then
        ipc.control(PMDGBaseVar+406, PMDG_ClkL)
        DspShow ("MSTR", "on")
    end
end

function NGX_AP_MASTER_off ()
    if ipc.readLvar('ngx_switch_406_a') ~= 100 then
        ipc.control(PMDGBaseVar+406, PMDG_ClkR)
        DspShow ("MSTR", "off")
    end
end

function NGX_ATHR_soft_disconnect ()
    ipc.control(PMDGBaseVar+682, PMDG_ClkL)
    DspShow ("ATHR", "soft")
end

function NGX_AP_soft_disconnect ()
    ipc.control(PMDGBaseVar+1004, PMDG_ClkL)
    DspShow (" AP ", "soft")
end

function NGX_AP_TOGA ()
    ipc.control(PMDGBaseVar+684, PMDG_ClkL)
    DspShow (" AP ", "TOGA")
end

-- ## Autopilot Knobs ###############

-- $$ AP Course

function NGX_AP_CRSL_set ()
    ipc.control(84132, tostring(OBS1))
end

function NGX_AP_CRSL_show ()
    OBS1 = ipc.readLvar("L:ngx_CRSwindowL")
    if _MCP1()  then
        DspShow("CRSL", OBS1)
    else
        DspCRS(OBS1)
    end
end

function NGX_AP_CRSR_show ()
    OBS2 = ipc.readLvar("L:ngx_CRSwindowR")
    if _MCP1()  then
        DspShow("CRSR", OBS2)
    else
        DspCRS(OBS2)
    end
end




-- $$ AP Heading

function NGX_AP_HDG_BANK_toggle ()
    -- toggling value form 1 to 2
    -- 1 - HDG, 2 - BANK
    mcp_hdg_mode = 3 - mcp_hdg_mode
    if mcp_hdg_mode == 1 then
        DspShow('HDG', '--')
    else
        NGX_AP_BANK_show ()
    end
end




function NGX_AP_HDG_show ()
    ngx_hdg = ipc.readLvar("L:ngx_HDGwindow")
    DspHDG(ngx_hdg)
end

function NGX_AP_BANK_show ()
local txt
local pos = ipc.readLvar('ngx_switch_389_a')
    if pos == 0 then txt = " 10"
    elseif pos == 10 then txt = " 15"
    elseif pos == 20 then txt = " 20"
    elseif pos == 30 then txt = " 25"
    elseif pos == 40 then txt = " 30"
    end
    DspShow ("BANK", txt)
end

function NGX_AP_BANK_inc ()
    ipc.control(PMDGBaseVar+389, PMDG_ClkR)
    NGX_AP_BANK_show ()
end

function NGX_AP_BANK_dec ()
    ipc.control(PMDGBaseVar+389, PMDG_ClkL)
    NGX_AP_BANK_show ()
end






-- ## Autopilot buttons ###############

function NGX_AP_N1 ()
    ipc.control(PMDGBaseVar+381, PMDG_ClkL)
    ipc.sleep(1)
    if _MCP1()  then
        DspShow (" AP ", " N1 ")
    else
        if ipc.readLvar('L:ngx_MCP_N1') == 1 then
            ngx_MCP_N1 = 1
            DspSPD_N1_on ()
        else
            ngx_MCP_N1 = 0
            DspSPD_N1_off ()
        end
    end
end

function NGX_AP_SPEED ()
    ipc.control(PMDGBaseVar+382, PMDG_ClkL)
    ipc.sleep(50)
    if _MCP1()  then
        DspShow (" AP ", " SPD")
    else
        DspSPD_N1_off ()
        if ipc.readLvar('L:ngx_MCP_Speed') == 1 then
            ngx_MCP_Speed = 1
            DspSPD_AP_on ()
        else
            ngx_MCP_Speed = 0
            DspSPD_AP_off ()
        end
    end
end

function NGX_AP_CO ()
    ipc.control(PMDGBaseVar+383, PMDG_ClkL)
    DspShow ("SPD", "C/O")
end

function NGX_AP_SPD_INTV ()
    ipc.control(PMDGBaseVar+387, PMDG_ClkL)
    DspShow ("SPD", "INTV")
end

function NGX_AP_ALT_INTV ()
    ipc.control(PMDGBaseVar+885, PMDG_ClkL)
    DspShow ("ALT", "INTV")
end

function NGX_AP_VNAV ()
    ipc.control(PMDGBaseVar+386, PMDG_ClkL)
    ipc.sleep(50)
    if _MCP1()  then
        DspShow (" AP ", "VNAV")
    else
        if ipc.readLvar("L:ngx_MCP_VNav") == 1 then
            DspVNAV_on ()
        else
            DspVNAV_off ()
        end
    end
end

function NGX_AP_LVLCHG ()
    ipc.control(PMDGBaseVar+391, PMDG_ClkL)
    ipc.sleep(50)
    if _MCP1()  then
        DspShow (" AP ", "LVLC")
    else
        if ipc.readLvar('L:ngx_MCP_LvlChg') == 1 then
            ngx_MCP_LvlChg = 1
            DspSPD_FLCH_on ()
        else
            ngx_MCP_LvlChg = 0
            DspSPD_FLCH_off ()
        end
   end
end

function NGX_AP_HDGSEL ()
    ipc.control(PMDGBaseVar+392, PMDG_ClkL)
    ipc.sleep(10)
    if _MCP1()  then
        DspShow (" HDG", " SEL")
    else
        if ipc.readLvar("L:ngx_MCP_HdgSel") == 1 then
            ngx_MCP_HdgSel = 1
            DspHDG_AP_on ()
        else
            ngx_MCP_HdgSel = 0
            DspHDG_AP_off ()
        end
    end
end

function NGX_AP_LNAV ()
    ipc.control(PMDGBaseVar+397, PMDG_ClkL)
    ipc.sleep(10)
    if _MCP1()  then
        DspShow (" AP ", "LNAV")
    else
        if ipc.readLvar("L:ngx_MCP_LNav") == 1 then
            DspLNAV_on ()
        else
            DspLNAV_off ()
        end
    end
end

function NGX_AP_VORLOC ()
    ipc.control(PMDGBaseVar+396, PMDG_ClkL)
    DspShow (" VOR", " LOC")
end

function NGX_AP_APP ()
    ipc.control(PMDGBaseVar+393, PMDG_ClkL)
    DspShow (" AP ", " APP")
end

function NGX_AP_ALTHLD ()
    ipc.control(PMDGBaseVar+394, PMDG_ClkL)
    ipc.sleep(10)
    if _MCP1()  then
        DspShow (" ALT", "HOLD")
    else
        if ipc.readLvar("L:ngx_MCP_AltHold") == 1 then
            ngx_MCP_AltHold = 1
            DspALT_AP_on ()
        else
            ngx_MCP_AltHold = 0
            DspALT_AP_off ()
        end
    end
end

function NGX_AP_VS ()
    ipc.control(PMDGBaseVar+395, PMDG_ClkL)
    ipc.sleep(10)
    if _MCP1()  then
        DspShow (" AP ", " V/S")
    else
        if ipc.readLvar("L:ngx_MCP_VS") == 1 then
            ngx_MCP_VS = 1
            DspVVS_AP_on ()
        else
            ngx_MCP_VS = 0
            DspVVS_AP_off ()
        end
    end
end

function NGX_AP_CMDA_toggle ()
    ipc.control(PMDGBaseVar+402, PMDG_ClkL)
    DspShow (" AP ", "CMDA")
end

function NGX_AP_CMDB_toggle ()
    ipc.control(PMDGBaseVar+403, PMDG_ClkL)
    DspShow (" AP ", "CMDB")
end

function NGX_AP_CWSA ()
    ipc.control(PMDGBaseVar+404, PMDG_ClkL)
    DspShow (" AP ", "CWSA")
end

function NGX_AP_CWSB ()
    ipc.control(PMDGBaseVar+405, PMDG_ClkL)
    DspShow (" AP ", "CWSB")
end

-- ## Overhead - Electrics ###############

-- $$ Ground Power

function NGX_GRD_PWR_on ()
    if ipc.readLvar('ngx_switch_17_a') ~= 100 then
        ipc.control(PMDGBaseVar+17, PMDG_ClkL)
		DspShow ("GPWR", "on", "GRD PWR", "on")
    end
end

function NGX_GRD_PWR_off ()
    if ipc.readLvar('ngx_switch_17_a') ~= 0 then
        ipc.control(PMDGBaseVar+17, PMDG_ClkR)
		DspShow ("GPWR", "off", "GRD PWR", "off")
    end
end

-- $$ Battery

function NGX_BAT_on ()
    if ipc.readLvar('ngx_switch_01_a') ~= 100 then
        if ipc.readLvar('ngx_switch_02_a') ~= 100 then
            ipc.control(PMDGBaseVar+2, PMDG_ClkR)
            ipc.sleep(50)
        end
        ipc.control(PMDGBaseVar+1, PMDG_ClkL)
        ipc.sleep(250)
        ipc.control(PMDGBaseVar+2, PMDG_ClkR)
        DspShow ("BAT", "on", "BATT", "on")
    end
end

function NGX_BAT_off ()
    if ipc.readLvar('ngx_switch_01_a') ~= 0 then
        if ipc.readLvar('ngx_switch_02_a') ~= 100 then
            ipc.control(PMDGBaseVar+2, PMDG_ClkR)
            ipc.sleep(50)
        end
        ipc.control(PMDGBaseVar+1, PMDG_ClkR)
        DspShow ("BAT", "off", "BATT", "off")
    end
end

function NGX_BAT_toggle ()
    if ipc.readLvar('ngx_switch_01_a') ~= 100 then
        NGX_BAT_on ()
    else
        NGX_BAT_off ()
    end
end

-- $$ DC Source

function NGX_DC_source_show ()
local txt
local pos = ipc.readLvar('ngx_switch_03_a')
    if pos == 0 then DCsTxt = "stby"
    elseif pos == 10 then txt = "BBus"
    elseif pos == 20 then txt = "Bat"
    elseif pos == 30 then txt = "AuxB"
    elseif pos == 40 then txt = "TR 1"
    elseif pos == 50 then txt = "TR 2"
    elseif pos == 60 then txt = "TR 3"
    elseif pos == 70 then txt = "test"
    end
    DspShow ("DC V", txt)
end

function NGX_DC_source_stby ()
    ipc.control(PMDGBaseVar+3, 0)
    ipc.sleep(50)
    NGX_DC_source_show ()
end

function NGX_DC_source_BBus ()
    ipc.control(PMDGBaseVar+3, 1)
    ipc.sleep(50)
    NGX_DC_source_show ()
end

function NGX_DC_source_Bat ()
    ipc.control(PMDGBaseVar+3, 2)
    ipc.sleep(50)
    NGX_DC_source_show ()
end

function NGX_DC_source_AuxB ()
    ipc.control(PMDGBaseVar+3, 3)
    ipc.sleep(50)
    NGX_DC_source_show ()
end

function NGX_DC_source_TR1 ()
    ipc.control(PMDGBaseVar+3, 4)
    ipc.sleep(50)
    NGX_DC_source_show ()
end

function NGX_DC_source_TR2 ()
    ipc.control(PMDGBaseVar+3, 5)
    ipc.sleep(50)
    NGX_DC_source_show ()
end

function NGX_DC_source_TR3 ()
    ipc.control(PMDGBaseVar+3, 6)
    ipc.sleep(50)
    NGX_DC_source_show ()
end

function NGX_DC_source_test ()
    ipc.control(PMDGBaseVar+3, 7)
    ipc.sleep(50)
    NGX_DC_source_show ()
end

function NGX_DC_source_inc ()
    ipc.control(PMDGBaseVar+3, PMDG_inc)
    NGX_DC_source_show ()
end

function NGX_DC_source_dec ()
    ipc.control(PMDGBaseVar+3, PMDG_dec)
    NGX_DC_source_show ()
end

---

function NGX_AC_source_show ()
local txt
local pos = ipc.readLvar('ngx_switch_04_a')
    if pos == 0 then txt = "stby"
    elseif pos == 10 then txt = "Grnd"
    elseif pos == 20 then txt = "Gen1"
    elseif pos == 30 then txt = "ApuG"
    elseif pos == 40 then txt = "Gen2"
    elseif pos == 50 then txt = "Inv"
    elseif pos == 60 then txt = "test"
    end
    DspShow ("AC V", txt)
end

function NGX_AC_source_stby ()
    ipc.control(PMDGBaseVar+4, 0)
    ipc.sleep(50)
    NGX_AC_source_show ()
end

function NGX_AC_source_Grnd ()
    ipc.control(PMDGBaseVar+4, 1)
    ipc.sleep(50)
    NGX_AC_source_show ()
end

function NGX_AC_source_Gen1 ()
    ipc.control(PMDGBaseVar+4, 2)
    ipc.sleep(50)
    NGX_AC_source_show ()
end

function NGX_AC_source_ApuG ()
    ipc.control(PMDGBaseVar+4, 3)
    ipc.sleep(50)
    NGX_AC_source_show ()
end

function NGX_AC_source_Gen2 ()
    ipc.control(PMDGBaseVar+4, 4)
    ipc.sleep(50)
    NGX_AC_source_show ()
end

function NGX_AC_source_Inv ()
    ipc.control(PMDGBaseVar+4, 5)
    ipc.sleep(50)
    NGX_AC_source_show ()
end

function NGX_AC_source_test ()
    ipc.control(PMDGBaseVar+4, 6)
    ipc.sleep(50)
    NGX_AC_source_show ()
end

function NGX_AC_source_inc ()
    if ipc.readLvar('ngx_switch_04_a') < 60 then
        ipc.control(PMDGBaseVar+4, PMDG_inc)
        NGX_AC_source_show ()
    end
end

function NGX_AC_source_dec ()
    if ipc.readLvar('ngx_switch_04_a') > 0 then
        ipc.control(PMDGBaseVar+4, PMDG_dec)
        NGX_AC_source_show ()
    end
end

-- $$ Standby Power

function NGX_STBY_POWER_bat ()
    if ipc.readLvar('ngx_switch_10_a') ~= 0 then
        if ipc.readLvar('ngx_switch_11_a') ~= 100 then
            ipc.control(PMDGBaseVar+11, PMDG_ClkR)
        end
        ipc.control(PMDGBaseVar+10, PMDG_ClkL)
        ipc.control(PMDGBaseVar+10, PMDG_ClkL)
        DspShow ("STBY", "bat")
    end
end

function NGX_STBY_POWER_off ()
    if ipc.readLvar('ngx_switch_10_a') == 0 then
        if ipc.readLvar('ngx_switch_11_a') ~= 100 then
            ipc.control(PMDGBaseVar+11, PMDG_ClkR)
            ipc.sleep(50)
        end
        ipc.control(PMDGBaseVar+10, PMDG_ClkR)
        DspShow ("STBY", "off")
    elseif ipc.readLvar('ngx_switch_10_a') == 100 then
        if ipc.readLvar('ngx_switch_11_a') ~= 100 then
            ipc.control(PMDGBaseVar+11, PMDG_ClkL)
            ipc.sleep(50)
        end
        ipc.control(PMDGBaseVar+10, PMDG_ClkL)
        DspShow ("STBY", "off")
    end
end

function NGX_STBY_POWER_auto ()
    if ipc.readLvar('ngx_switch_10_a') ~= 100 then
        if ipc.readLvar('ngx_switch_11_a') ~= 100 then
            ipc.control(PMDGBaseVar+11, PMDG_ClkR)
        end
        ipc.control(PMDGBaseVar+10, PMDG_ClkR)
        ipc.sleep(250)
        ipc.control(PMDGBaseVar+11, PMDG_ClkR)
        DspShow ("STBY", "auto")
    end
end

-- $$ Bus Transfer

function NGX_BUS_TRANS_off ()
    if ipc.readLvar('ngx_switch_18_a') ~= 0 then
        if ipc.readLvar('ngx_switch_19_a') ~= 100 then
            ipc.control(PMDGBaseVar+19, PMDG_ClkR)
        end
        ipc.control(PMDGBaseVar+18, PMDG_ClkL)
        --ipc.control(PMDGBaseVar+18, PMDG_ClkR)
        DspShow ("BUSX", "off")
    end
end

function NGX_BUS_TRANS_Auto ()
    if ipc.readLvar('ngx_switch_18_a') ~= 100 then
        if ipc.readLvar('ngx_switch_19_a') ~= 100 then
            ipc.control(PMDGBaseVar+19, PMDG_ClkR)
        end
        ipc.control(PMDGBaseVar+18, PMDG_ClkR)
        DspShow ("BUSX", "auto")
    end
    if ipc.readLvar('ngx_switch_19_a') ~= 0 then
        ipc.sleep(250)
        ipc.control(PMDGBaseVar+19, PMDG_ClkR)
    end
end

-- $$ APU

function NGX_APU_show ()
local txt
local pos = ipc.readLvar('ngx_switch_118_a')
    if pos == 0 then txt = "off"
    elseif pos == 50 then txt = "on"
    elseif pos > 50 then txt = "strt"
    end
    DspShow ("APU", txt)
end

function NGX_APU_off ()
    if ipc.readLvar('ngx_switch_118_a') ~= 0 then
        ipc.control(PMDGBaseVar+118, PMDG_ClkR)
        NGX_APU_show()
    end
end

function NGX_APU_on ()
    if ipc.readLvar('ngx_switch_118_a') < 50 then
        ipc.control(PMDGBaseVar+118, PMDG_ClkL)
        NGX_APU_show()
    end
end

function NGX_APU_ShowEGT ()
    ApuEgt = ipc.readLvar('ngx_switch_35_a')
    if ApuEgt > 0 then
        DspShow("APU", ApuEgt .. 'C')
    end
end

function NGX_APU_start()
    if ipc.readLvar('ngx_switch_118_a') == 50 then
        ipc.control(PMDGBaseVar+118, PMDG_ClkL)
        NGX_APU_show()
    end
end

function NGX_APU_dec ()
    if ipc.readLvar('ngx_switch_118_a') > 0 then
        ipc.control(PMDGBaseVar+118, PMDG_ClkR)
        NGX_APU_show()
    end
end

function NGX_APU_inc ()
    if ipc.readLvar('ngx_switch_118_a') < 100 then
        ipc.control(PMDGBaseVar+118, PMDG_ClkL)
        NGX_APU_show()
    end
end

function NGX_APU_GEN_L_on ()
    if ipc.readLvar('ngx_switch_28_a') ~= 100 then
        ipc.control(PMDGBaseVar+28, PMDG_ClkL)
		DspShow ("aGEN", "L on", "APU GEN", "Left on")
    end
end

function NGX_APU_GEN_L_off ()
    if ipc.readLvar('ngx_switch_28_a') ~= 0 then
        ipc.control(PMDGBaseVar+28, PMDG_ClkR)
		DspShow ("aGEN", "Loff", "APU GEN", "Left off")
    end
end

function NGX_APU_GEN_R_on ()
    if ipc.readLvar('ngx_switch_29_a') ~= 100 then
        ipc.control(PMDGBaseVar+29, PMDG_ClkL)
		DspShow ("aGEN", "R on", "APU GEN", "Right on")
    end
end

function NGX_APU_GEN_R_off ()
    if ipc.readLvar('ngx_switch_29_a') ~= 0 then
        ipc.control(PMDGBaseVar+29, PMDG_ClkR)
		DspShow ("aGEN", "Roff", "APU GEN", "Rght off")
    end
end

function NGX_APU_GEN_BOTH_on ()
    NGX_APU_GEN_L_on ()
    _sleep(100, 200)
    NGX_APU_GEN_R_on ()
    ipc.sleep(50)
end

function NGX_APU_GEN_BOTH_off ()
    NGX_APU_GEN_L_off ()
    _sleep(100, 200)
    NGX_APU_GEN_R_off ()
    ipc.sleep(50)
end

-- $$ Generators

function NGX_GEN_L_on ()
    if ipc.readLvar('ngx_switch_27_a') ~= 100 then
        ipc.control(PMDGBaseVar+27, PMDG_ClkL)
		DspShow ("GEN", "L on")
    end
end

function NGX_GEN_L_off ()
    if ipc.readLvar('ngx_switch_27_a') ~= 0 then
        ipc.control(PMDGBaseVar+27, PMDG_ClkR)
		DspShow ("GEN", "Loff")
    end
end

function NGX_GEN_R_on ()
    if ipc.readLvar('ngx_switch_30_a') ~= 100 then
        ipc.control(PMDGBaseVar+30, PMDG_ClkL)
		DspShow ("GEN", "R on")
    end
end

function NGX_GEN_R_off ()
    if ipc.readLvar('ngx_switch_30_a') ~= 0 then
        ipc.control(PMDGBaseVar+30, PMDG_ClkR)
		DspShow ("GEN", "Roff")
    end
end

function NGX_GEN_BOTH_on ()
    NGX_GEN_L_on ()
    _sleep(100, 200)
    NGX_GEN_R_on ()
    ipc.sleep(50)
end

function NGX_GEN_BOTH_off ()
    NGX_GEN_L_off ()
    _sleep(100, 200)
    NGX_GEN_R_off ()
    ipc.sleep(50)
end

-- ## Overhead - Fuel ###############

function NGX_PUMP1_AFT_on ()
    if ipc.readLvar('ngx_switch_37_a') ~= 100 then
        ipc.control(PMDGBaseVar+37, PMDG_ClkL)
		DspShow("PMP1", "A on")
    end
end

function NGX_PUMP1_AFT_off ()
    if ipc.readLvar('ngx_switch_37_a') ~= 0 then
        ipc.control(PMDGBaseVar+37, PMDG_ClkR)
		DspShow("PMP1", "Aoff")
    end
end

function NGX_PUMP1_AFT_toggle ()
    if ipc.readLvar('ngx_switch_37_a') == 100 then
        NGX_PUMP1_AFT_off()
    else
        NGX_PUMP1_AFT_on()
    end
end

function NGX_PUMP1_FWD_on ()
    if ipc.readLvar('ngx_switch_38_a') ~= 100 then
        ipc.control(PMDGBaseVar+38, PMDG_ClkL)
		DspShow("PMP1", "F on")
    end
end

function NGX_PUMP1_FWD_off ()
    if ipc.readLvar('ngx_switch_38_a') ~= 0 then
        ipc.control(PMDGBaseVar+38, PMDG_ClkR)
		DspShow("PMP1", "Foff")
    end
end

function NGX_PUMP1_FWD_toggle ()
    if ipc.readLvar('ngx_switch_38_a') == 100 then
        NGX_PUMP1_FWD_off()
    else
        NGX_PUMP1_FWD_on()
    end
end

function NGX_PUMP2_FWD_on ()
    if ipc.readLvar('ngx_switch_39_a') ~= 100 then
        ipc.control(PMDGBaseVar+39, PMDG_ClkL)
		DspShow("PMP2", "F on")
    end
end

function NGX_PUMP2_FWD_off ()
    if ipc.readLvar('ngx_switch_39_a') ~= 0 then
        ipc.control(PMDGBaseVar+39, PMDG_ClkR)
		DspShow("PMP2", "Foff")
    end
end

function NGX_PUMP2_FWD_toggle ()
    if ipc.readLvar('ngx_switch_39_a') == 100 then
        NGX_PUMP2_FWD_off()
    else
        NGX_PUMP2_FWD_on()
    end
end

function NGX_PUMP2_AFT_on ()
    if ipc.readLvar('ngx_switch_40_a') ~= 100 then
        ipc.control(PMDGBaseVar+40, PMDG_ClkL)
		DspShow("PMP2", "A on")
    end
end

function NGX_PUMP2_AFT_off ()
    if ipc.readLvar('ngx_switch_40_a') ~= 0 then
        ipc.control(PMDGBaseVar+40, PMDG_ClkR)
		DspShow("PMP2", "Aoff")
    end
end

function NGX_PUMP2_AFT_toggle ()
    if ipc.readLvar('ngx_switch_40_a') == 100 then
        NGX_PUMP2_AFT_off()
    else
        NGX_PUMP2_AFT_on()
    end
end

function NGX_PUMPCTR_L_on ()
    if ipc.readLvar('ngx_switch_45_a') ~= 100 then
        ipc.control(PMDGBaseVar+45, PMDG_ClkL)
		DspShow("PMPC", "L on")
    end
end

function NGX_PUMPCTR_L_off ()
    if ipc.readLvar('ngx_switch_45_a') ~= 0 then
        ipc.control(PMDGBaseVar+45, PMDG_ClkR)
		DspShow("PMPC", "Loff")
    end
end

function NGX_PUMPCTR_L_toggle ()
    if ipc.readLvar('ngx_switch_45_a') == 100 then
        NGX_PUMPCTR_L_off()
    else
        NGX_PUMPCTR_L_on()
    end
end

function NGX_PUMPCTR_R_on ()
    if ipc.readLvar('ngx_switch_46_a') ~= 100 then
        ipc.control(PMDGBaseVar+46, PMDG_ClkL)
		DspShow("PMPC", "R on")
    end
end

function NGX_PUMPCTR_R_off ()
    if ipc.readLvar('ngx_switch_46_a') ~= 0 then
        ipc.control(PMDGBaseVar+46, PMDG_ClkR)
		DspShow("PMPC", "Roff")
    end
end

function NGX_PUMPCTR_R_toggle ()
    if ipc.readLvar('ngx_switch_46_a') == 100 then
        NGX_PUMPCTR_R_off()
    else
        NGX_PUMPCTR_R_on()
    end
end

function NGX_PUMPS1_on ()
    NGX_PUMP1_AFT_on ()
    -- _sleep(200, 400)
    NGX_PUMP1_FWD_on ()
end

function NGX_PUMPS1_off ()
    NGX_PUMP1_AFT_off ()
    -- _sleep(200, 400)
    NGX_PUMP1_FWD_off ()
end

function NGX_PUMPS1_toggle ()
    NGX_PUMP1_AFT_toggle ()
    -- _sleep(200, 400)
    NGX_PUMP1_FWD_toggle ()
end

function NGX_PUMPS2_on ()
    NGX_PUMP2_FWD_on ()
    -- _sleep(200, 400)
    NGX_PUMP2_AFT_on ()
end

function NGX_PUMPS2_off ()
    NGX_PUMP2_FWD_off ()
    -- _sleep(200, 400)
    NGX_PUMP2_AFT_off ()
end

function NGX_PUMPS2_toggle ()
    NGX_PUMP2_AFT_toggle ()
    -- _sleep(200, 400)
    NGX_PUMP2_FWD_toggle ()
end

function NGX_PUMPSCTR_on ()
    NGX_PUMPCTR_L_on ()
    -- _sleep(200, 400)
    NGX_PUMPCTR_R_on ()
end

function NGX_PUMPSCTR_off ()
    NGX_PUMPCTR_L_off ()
    -- _sleep(200, 400)
    NGX_PUMPCTR_R_off ()
end

function NGX_PUMPSCTR_toggle ()
    NGX_PUMPCTR_L_toggle ()
    -- _sleep(200, 400)
    NGX_PUMPCTR_R_toggle ()
end

function NGX_PUMPS1and2_on ()
    NGX_PUMPS1_on ()
    -- _sleep(200, 400)
    NGX_PUMPS2_on ()
end

function NGX_PUMPS1and2_off ()
    NGX_PUMPS1_off ()
    -- _sleep(200, 400)
    NGX_PUMPS2_off ()
end

function NGX_CROSSFEED_feed ()
    if ipc.readLvar('ngx_switch_49_a') ~= 0 then
        ipc.control(PMDGBaseVar+49, PMDG_ClkL)
        DspShow("X-FD", "on")
    end
end

function NGX_CROSSFEED_off ()
    if ipc.readLvar('ngx_switch_49_a') ~= 100 then
        ipc.control(PMDGBaseVar+49, PMDG_ClkR)
        DspShow("X-FD", "off")
    end
end

function NGX_CROSSFEED_toggle ()
    if ipc.readLvar('ngx_switch_49_a') ~= 0 then
        NGX_CROSSFEED_feed()
    else
        NGX_CROSSFEED_off()
    end
end

-- ## Inerial Reference System (IRS) ###############

function NGX_IRS_R_show ()
local txt
local pos = ipc.readLvar("L:ngx_switch_256_a")
    if pos == 0 then pos = "off"
    elseif pos == 10 then txt = "algn"
    elseif pos == 20 then txt = "nav"
    elseif pos == 30 then txt ="att"
    end
    DspShow("IRSR", txt)
end

function NGX_IRS_R_inc ()
local pos = ipc.readLvar('ngx_switch_256_a')
    if pos < 30 then
        ipc.control(PMDGBaseVar+256, PMDG_inc)
		NGX_IRS_R_show ()
    end
end

function NGX_IRS_R_dec ()
local pos = ipc.readLvar('ngx_switch_256_a')
    if pos > 0 then
        ipc.control(PMDGBaseVar+256, PMDG_dec)
		NGX_IRS_R_show ()
    end
end

function NGX_IRS_R_calc (sel)
if sel == nil then return end
local pos = ipc.readLvar("L:ngx_switch_256_a")/10
    if pos > sel then
        for i = 1, (pos - sel), 1 do
            NGX_IRS_R_dec ()
            ipc.sleep(50)
        end
    elseif pos < sel then
        for i = 1, (sel - pos), 1 do
            NGX_IRS_R_inc ()
            ipc.sleep(50)
        end
    end
end

function NGX_IRS_R_off ()
    if ipc.readLvar('ngx_switch_256_a') ~= 0 then
        NGX_IRS_R_calc(0)
        NGX_IRS_R_show()
    end
end

function NGX_IRS_R_align ()
    if ipc.readLvar('ngx_switch_256_a') ~= 10 then
        NGX_IRS_R_calc(1)
        NGX_IRS_R_show()
    end
end

function NGX_IRS_R_nav ()
    if ipc.readLvar('ngx_switch_256_a') ~= 20 then
        NGX_IRS_R_calc(2)
        NGX_IRS_R_show()
    end
end

function NGX_IRS_R_att ()
    if ipc.readLvar('ngx_switch_256_a') ~= 30 then
        NGX_IRS_R_calc(3)
        NGX_IRS_R_show()
    end
end

------------------------------

function NGX_IRS_L_show ()
local txt
local pos = ipc.readLvar("L:ngx_switch_255_a")
    if pos == 0 then txt = "OFF"
    elseif pos == 10 then txt = "ALGN"
    elseif pos == 20 then txt = "NAV"
    elseif pos == 30 then txt = "ATT"
    end
    DspShow("IRSL", txt)
end

function NGX_IRS_L_inc ()
local pos = ipc.readLvar('ngx_switch_255_a')
    if pos < 30 then
        ipc.control(PMDGBaseVar+255, PMDG_inc)
		NGX_IRS_L_show ()
    end
end

function NGX_IRS_L_dec ()
local pos = ipc.readLvar('ngx_switch_255_a')
    if pos > 0 then
        ipc.control(PMDGBaseVar+255, PMDG_dec)
		NGX_IRS_L_show ()
    end
end

function NGX_IRS_L_calc (sel)
if sel == nil then return end
local pos = ipc.readLvar("L:ngx_switch_255_a")/10
    if pos > sel then
        for i = 1, (pos - sel), 1 do
            NGX_IRS_L_dec ()
        end
    elseif pos < sel then
        for i = 1, (sel - pos), 1 do
            NGX_IRS_L_inc ()
        end
    end
end

function NGX_IRS_L_off ()
    if ipc.readLvar('ngx_switch_255_a') ~= 0 then
        NGX_IRS_L_calc(0)
        NGX_IRS_L_show()
    end
end

function NGX_IRS_L_align ()
    if ipc.readLvar('ngx_switch_255_a') ~= 10 then
        NGX_IRS_L_calc(1)
        NGX_IRS_L_show()
    end
end

function NGX_IRS_L_nav ()
    if ipc.readLvar('ngx_switch_255_a') ~= 20 then
        NGX_IRS_L_calc(2)
        NGX_IRS_L_show()
    end
end

function NGX_IRS_L_att ()
    if ipc.readLvar('ngx_switch_255_a') ~= 30 then
        NGX_IRS_L_calc(3)
        NGX_IRS_L_show()
    end
end

---

function NGX_IRS_both_inc ()
    NGX_IRS_L_inc ()
    NGX_IRS_R_inc ()
end

function NGX_IRS_both_dec ()
    NGX_IRS_L_dec ()
    NGX_IRS_R_dec ()
end

function NGX_IRS_both_off ()
    NGX_IRS_L_off ()
    NGX_IRS_R_of ()
end

function NGX_IRS_both_align ()
    NGX_IRS_L_align ()
    NGX_IRS_R_align ()
end

function NGX_IRS_both_nav ()
    NGX_IRS_L_nav ()
    NGX_IRS_R_nav ()
end

function NGX_IRS_both_att ()
    NGX_IRS_L_att ()
    NGX_IRS_R_att ()
end

-- ## Overhead - Window Heat ###############

-- Left Side

function NGX_W_HEAT_L_SIDE_on ()
    if ipc.readLvar('ngx_switch_135_a') ~= 100 then
        ipc.control(PMDGBaseVar+135, PMDG_ClkL)
		DspShow ("WHTs", "L on")
    end
end

function NGX_W_HEAT_L_SIDE_off ()
    if ipc.readLvar('ngx_switch_135_a') ~= 0 then
        ipc.control(PMDGBaseVar+135, PMDG_ClkR)
		DspShow ("WHTs", "Loff")
    end
end

function NGX_W_HEAT_L_SIDE_toggle ()
	if _tl("ngx_switch_135_a", 0) then
        NGX_W_HEAT_L_SIDE_on ()
	else
        NGX_W_HEAT_L_SIDE_off ()
	end
end

-- Left FWD

function NGX_W_HEAT_L_FWD_on ()
    if ipc.readLvar('ngx_switch_136_a') ~= 100 then
        ipc.control(PMDGBaseVar+136, PMDG_ClkL)
        DspShow ("WHTf", "L on")
    end
end

function NGX_W_HEAT_L_FWD_off ()
    if ipc.readLvar('ngx_switch_136_a') ~= 0 then
        ipc.control(PMDGBaseVar+136, PMDG_ClkR)
		DspShow ("WHTf", "Loff")
    end
end

function NGX_W_HEAT_L_FWD_toggle ()
	if _tl("ngx_switch_136_a", 0) then
        NGX_W_HEAT_L_FWD_on ()
	else
        NGX_W_HEAT_L_FWD_off ()
	end
end

--------

function NGX_W_HEAT_TEST_test ()
    if ipc.readLvar('ngx_switch_137_a') ~= 100 then
        ipc.control(PMDGBaseVar+137, PMDG_ClkL)
        ipc.control(PMDGBaseVar+137, PMDG_ClkL)
        DspShow ("W HT", "test")
    end
end

function NGX_W_HEAT_TEST_off ()
local pos = ipc.readLvar('ngx_switch_137_a')
    if pos < 50 then
        ipc.control(PMDGBaseVar+137, PMDG_ClkL)
        DspShow ("W HT", " off")
    elseif pos > 50 then
        ipc.control(PMDGBaseVar+137, PMDG_ClkR)
        DspShow ("W HT", " off")
    end
end

function NGX_W_HEAT_TEST_ovht ()
local pos = ipc.readLvar('ngx_switch_137_a')
    if pos ~= 0 then
        ipc.control(PMDGBaseVar+137, PMDG_ClkR)
        ipc.control(PMDGBaseVar+137, PMDG_ClkR)
        DspShow ("W HT", "ovht")
    end
end

-- Right FWD

function NGX_W_HEAT_R_FWD_on ()
    if ipc.readLvar('ngx_switch_138_a') ~= 100 then
        ipc.control(PMDGBaseVar+138, PMDG_ClkL)
		DspShow ("WHTf", "R on")
    end
end

function NGX_W_HEAT_R_FWD_off ()
    if ipc.readLvar('ngx_switch_138_a') ~=0 then
        ipc.control(PMDGBaseVar+138, PMDG_ClkR)
		DspShow ("WHTf", "Roff")
    end
end

function NGX_W_HEAT_R_FWD_toggle ()
	if _tl("ngx_switch_138_a", 0) then
        NGX_W_HEAT_R_FWD_on ()
	else
        NGX_W_HEAT_R_FWD_off ()
	end
end

-- Right Side

function NGX_W_HEAT_R_SIDE_on ()
    if ipc.readLvar('ngx_switch_139_a') ~= 100 then
        ipc.control(PMDGBaseVar+139, PMDG_ClkL)
		DspShow ("WHTs", "R on")
    end
end

function NGX_W_HEAT_R_SIDE_off ()
    if ipc.readLvar('ngx_switch_139_a') ~= 0 then
        ipc.control(PMDGBaseVar+139, PMDG_ClkR)
		DspShow ("WHTs", "Roff")
    end
end

function NGX_W_HEAT_R_SIDE_toggle ()
	if _tl("ngx_switch_139_a", 0) then
        NGX_W_HEAT_R_SIDE_on ()
	else
        NGX_W_HEAT_R_SIDE_off ()
	end
end

-- Left

function NGX_W_HEAT_ALL_LEFT_on ()
    NGX_W_HEAT_L_SIDE_on ()
    _sleep(150, 250)
    NGX_W_HEAT_L_FWD_on ()
end

function NGX_W_HEAT_ALL_LEFT_off ()
    NGX_W_HEAT_L_SIDE_off ()
    _sleep(150, 250)
    NGX_W_HEAT_L_FWD_off ()
end

function NGX_W_HEAT_ALL_LEFT_toggle ()
    NGX_W_HEAT_L_SIDE_toggle ()
    _sleep(150, 250)
    NGX_W_HEAT_L_FWD_toggle ()
end

-- Right

function NGX_W_HEAT_ALL_RIGHT_on ()
    NGX_W_HEAT_R_FWD_on ()
    _sleep(150, 250)
    NGX_W_HEAT_R_SIDE_on ()
end

function NGX_W_HEAT_ALL_RIGHT_off ()
    NGX_W_HEAT_R_FWD_off ()
    _sleep(150, 250)
    NGX_W_HEAT_R_SIDE_off ()
end

function NGX_W_HEAT_ALL_RIGHT_toggle ()
    NGX_W_HEAT_R_FWD_toggle ()
    _sleep(150, 250)
    NGX_W_HEAT_R_SIDE_toggle ()
end

-- all

function NGX_W_HEAT_ALL_on ()
    NGX_W_HEAT_ALL_LEFT_on ()
    _sleep(150, 250)
    NGX_W_HEAT_ALL_RIGHT_on ()
end

function NGX_W_HEAT_ALL_off ()
    NGX_W_HEAT_ALL_LEFT_off ()
    _sleep(150, 250)
    NGX_W_HEAT_ALL_RIGHT_off ()
end

function NGX_W_HEAT_ALL_toggle ()
    NGX_W_HEAT_ALL_LEFT_toggle ()
    _sleep(150, 250)
    NGX_W_HEAT_ALL_RIGHT_toggle ()
end

-- ## Overhead - Probe Heat ###############

-- Left

function NGX_PROBE_HEAT_L_on ()
    if ipc.readLvar('ngx_switch_140_a') ~= 100 then
        ipc.control(PMDGBaseVar+140, PMDG_ClkL)
		DspShow ("Prbe", "L on")
    end
end

function NGX_PROBE_HEAT_L_off ()
    if ipc.readLvar('ngx_switch_140_a') ~= 0 then
        ipc.control(PMDGBaseVar+140, PMDG_ClkR)
		DspShow ("Prbe", "Loff")
    end
end

function NGX_PROBE_HEAT_L_toggle ()
	if _tl("ngx_switch_140_a", 0) then
        NGX_PROBE_HEAT_L_on ()
	else
        NGX_PROBE_HEAT_L_off ()
	end
end

-- Right

function NGX_PROBE_HEAT_R_on ()
    if ipc.readLvar('ngx_switch_141_a') ~= 100 then
        ipc.control(PMDGBaseVar+141, PMDG_ClkL)
		DspShow ("Prbe", "R on")
    end
end

function NGX_PROBE_HEAT_R_off ()
    if ipc.readLvar('ngx_switch_141_a') ~= 0 then
        ipc.control(PMDGBaseVar+141, PMDG_ClkR)
		DspShow ("Prbe", "Roff")
    end
end

function NGX_PROBE_HEAT_R_toggle ()
	if _tl("ngx_switch_141_a", 0) then
        NGX_PROBE_HEAT_R_on ()
	else
        NGX_PROBE_HEAT_R_off ()
	end
end

function NGX_PROBE_HEAT_BOTH_on ()
    NGX_PROBE_HEAT_L_on ()
    --_sleep(15, 25)
    NGX_PROBE_HEAT_R_on ()
end

function NGX_PROBE_HEAT_BOTH_off ()
    NGX_PROBE_HEAT_L_off ()
    --_sleep(15, 25)
    NGX_PROBE_HEAT_R_off ()
end

function NGX_PROBE_HEAT_BOTH_toggle ()
    NGX_PROBE_HEAT_L_toggle ()
    _sleep(15, 25)
    NGX_PROBE_HEAT_R_toggle ()
end

-- ## Overhead - Anti Ice ###############

function NGX_ANTI_ICE_WING_on ()
    if ipc.readLvar('ngx_switch_156_a') ~= 100 then
        ipc.control(PMDGBaseVar+156, PMDG_ClkL)
		DspShow ("AICE", "W on")
    end
end

function NGX_ANTI_ICE_WING_off ()
    if ipc.readLvar('ngx_switch_156_a') ~= 0 then
        ipc.control(PMDGBaseVar+156, PMDG_ClkR)
		DspShow ("AICE", "Woff")
    end
end

function NGX_ANTI_ICE_WING_toggle ()
	if _tl("ngx_switch_156_a", 0) then
        NGX_ANTI_ICE_WING_on ()
	else
        NGX_ANTI_ICE_WING_off ()
	end
end

function NGX_ANTI_ICE_ENG_1_on ()
    if ipc.readLvar('ngx_switch_157_a') ~= 100 then
        ipc.control(PMDGBaseVar+157, PMDG_ClkL)
		DspShow ("ICEE", "1 on")
    end
end

function NGX_ANTI_ICE_ENG_1_off ()
    if ipc.readLvar('ngx_switch_157_a') ~= 0 then
        ipc.control(PMDGBaseVar+157, PMDG_ClkR)
		DspShow ("ICEE", "1off")
    end
end

function NGX_ANTI_ICE_ENG_1_toggle ()
	if _tl("ngx_switch_157_a", 0) then
        NGX_ANTI_ICE_ENG_1_on ()
	else
        NGX_ANTI_ICE_ENG_1_off ()
	end
end

function NGX_ANTI_ICE_ENG_2_on ()
    if ipc.readLvar('ngx_switch_158_a') ~= 100 then
        ipc.control(PMDGBaseVar+158, PMDG_ClkL)
		DspShow ("ICEE", "2 on")
    end
end

function NGX_ANTI_ICE_ENG_2_off ()
    if ipc.readLvar('ngx_switch_158_a') ~= 0 then
        ipc.control(PMDGBaseVar+158, PMDG_ClkR)
		DspShow ("ICEE", "2off")
    end
end

function NGX_ANTI_ICE_ENG_2_toggle ()
	if _tl("ngx_switch_158_a", 0) then
        NGX_ANTI_ICE_ENG_2_on ()
	else
       NGX_ANTI_ICE_ENG_2_off ()
	end
end

function NGX_ANTI_ICE_ENG_both_on ()
    NGX_ANTI_ICE_ENG_1_on ()
    --_sleep(150, 250)
    NGX_ANTI_ICE_ENG_2_on ()
end

function NGX_ANTI_ICE_ENG_both_off ()
    NGX_ANTI_ICE_ENG_1_off ()
    --_sleep(150, 250)
    NGX_ANTI_ICE_ENG_2_off ()
end

function NGX_ANTI_ICE_ENG_both_toggle ()
    NGX_ANTI_ICE_ENG_1_toggle ()
    --_sleep(150, 250)
    NGX_ANTI_ICE_ENG_2_toggle ()
end

function NGX_ANTI_ICE_ENG_WING_on ()
    NGX_ANTI_ICE_WING_on ()
    --_sleep(150, 250)
    NGX_ANTI_ICE_ENG_1_on ()
    --_sleep(150, 250)
    NGX_ANTI_ICE_ENG_2_on ()
end

function NGX_ANTI_ICE_ENG_WING_off ()
    NGX_ANTI_ICE_WING_off ()
    --_sleep(150, 250)
    NGX_ANTI_ICE_ENG_1_off ()
    --_sleep(150, 250)
    NGX_ANTI_ICE_ENG_2_off ()
end

function NGX_ANTI_ICE_ENG_WING_toggle ()
    NGX_ANTI_ICE_WING_toggle ()
    --_sleep(100, 100)
    NGX_ANTI_ICE_ENG_1_toggle ()
    --_sleep(100, 100)
    NGX_ANTI_ICE_ENG_2_toggle ()
end

-- ## Overhead - Hydraulics ###############

-- Hyd A Eng1

function NGX_HYD_A_Eng1_on ()
    if ipc.readLvar('ngx_switch_165_a') ~= 100 then
        ipc.control(PMDGBaseVar+165, PMDG_ClkL)
        DspShow ("HydA", "1 on")
    end
end

function NGX_HYD_A_Eng1_off ()
    if ipc.readLvar('ngx_switch_165_a') ~= 0 then
        ipc.control(PMDGBaseVar+165, PMDG_ClkR)
        DspShow ("HydA", "1off")
    end
end

function NGX_HYD_A_Eng1_toggle ()
	if _tl("ngx_switch_165_a", 0) then
        NGX_HYD_A_Eng1_on ()
	else
        NGX_HYD_A_Eng1_off ()
	end
end

-- Hyd A Elec2

function NGX_HYD_A_Elec2_on ()
    if ipc.readLvar('ngx_switch_167_a') ~= 100 then
        ipc.control(PMDGBaseVar+167, PMDG_ClkL)
		DspShow ("HydA", "2 on")
    end
end

function NGX_HYD_A_Elec2_off ()
    if ipc.readLvar('ngx_switch_167_a') ~= 0 then
        ipc.control(PMDGBaseVar+167, PMDG_ClkR)
		DspShow ("HydA", "2off")
    end
end

function NGX_HYD_A_Elec2_toggle ()
	if _tl("ngx_switch_167_a", 0) then
        NGX_HYD_A_Elec2_on ()
	else
        NGX_HYD_A_Elec2_off ()
	end
end

-- Hyd B Elec1

function NGX_HYD_B_Elec1_on ()
    if ipc.readLvar('ngx_switch_168_a') ~= 100 then
        ipc.control(PMDGBaseVar+168, PMDG_ClkL)
		DspShow ("HydB", "1 on")
    end
end

function NGX_HYD_B_Elec1_off ()
    if ipc.readLvar('ngx_switch_168_a') ~= 0 then
        ipc.control(PMDGBaseVar+168, PMDG_ClkR)
		DspShow ("HydB", "1off")
    end
end

function NGX_HYD_B_Elec1_toggle ()
	if _tl("ngx_switch_168_a", 0) then
        NGX_HYD_B_Elec1_on ()
	else
        NGX_HYD_B_Elec1_off ()
	end
end

-- Hyd B Eng2

function NGX_HYD_B_Eng2_on ()
    if ipc.readLvar('ngx_switch_166_a') ~= 100 then
        ipc.control(PMDGBaseVar+166, PMDG_ClkL)
		DspShow ("HydB", "2 on")
    end
end

function NGX_HYD_B_Eng2_off ()
    if ipc.readLvar('ngx_switch_166_a') ~= 0 then
        ipc.control(PMDGBaseVar+166, PMDG_ClkR)
		DspShow ("HydB", "2off")
    end
end

function NGX_HYD_B_Eng2_toggle ()
	if _tl("ngx_switch_166_a", 0) then
        NGX_HYD_B_Eng2_on ()
	else
        NGX_HYD_B_Eng2_off ()
	end
end

function NGX_HYD_ENG_Both_on ()
    NGX_HYD_A_Eng1_on ()
    _sleep(150, 250)
    NGX_HYD_B_Eng2_on ()
end

function NGX_HYD_ENG_Both_off ()
    NGX_HYD_A_Eng1_off ()
    _sleep(150, 250)
    NGX_HYD_B_Eng2_off ()
end

function NGX_HYD_ENG_Both_toggle ()
    NGX_HYD_A_Eng1_toggle ()
    _sleep(150, 250)
    NGX_HYD_B_Eng2_toggle ()
end

function NGX_HYD_ELEC_Both_on ()
    NGX_HYD_A_Elec2_on ()
    _sleep(150, 250)
    NGX_HYD_B_Elec1_on ()
end

function NGX_HYD_ELEC_Both_off ()
    NGX_HYD_A_Elec2_off ()
    _sleep(150, 250)
    NGX_HYD_B_Elec1_off ()
end

function NGX_HYD_ELEC_Both_toggle ()
    NGX_HYD_A_Elec2_toggle ()
    _sleep(150, 250)
    NGX_HYD_B_Elec1_toggle ()
end

-- ## Overhead - Flt & Land Alt ###############

-- IMPORTANT - don't use mouse to adust these values

function NGX_FLTALT_inc ()
    ipc.control(69850, PMDG_inc)
    if FLTALTvar < 420 then
        FLTALTvar = FLTALTvar + 5
        if FLTALTvar > 420 then
            FLTALTvar = 420
        end
    end
    ipc.control(0x148AB, FLTALTvar * 100)
    if _MCP1() then
        DspShow("FAlt", FLTALTvar)
    else
        DspMed1("FLT ALT")
        DspMed2(FLTALTvar * 100)
    end
    _sleep(50)
end

function NGX_FLTALT_incfast ()
    ipc.control(69850, PMDG_inc)
    if FLTALTvar < 420 then
        FLTALTvar = FLTALTvar + 50
        if FLTALTvar > 420 then
            FLTALTvar = 420
        end
    end
    ipc.control(0x148AB, FLTALTvar * 100)
    if _MCP1() then
        DspShow("FAlt", FLTALTvar)
    else
        DspMed1("FLT ALT")
        DspMed2(FLTALTvar * 100)
    end
    _sleep(50)
end

function NGX_FLTALT_dec ()
    ipc.control(69850, PMDG_dec)
    if FLTALTvar > -10 then
        FLTALTvar = FLTALTvar - 5
        if FLTALTvar < -10 then
            FLTALTvar = -10
        end
    end
    ipc.control(0x148AB, FLTALTvar * 100)
    if _MCP1() then
        DspShow("FAlt", FLTALTvar)
    else
        DspMed1("FLT ALT")
        DspMed2(FLTALTvar * 100)
    end
    _sleep(50)
end

function NGX_FLTALT_decfast ()
    ipc.control(69850, PMDG_dec)
    if FLTALTvar > -10 then
        FLTALTvar = FLTALTvar - 50
        if FLTALTvar < -10 then
            FLTALTvar = -10
        end
    end
    ipc.control(0x148AB, FLTALTvar * 100)
    if _MCP1() then
        DspShow("FAlt", FLTALTvar)
    else
        DspMed1("FLT ALT")
        DspMed2(FLTALTvar * 100)
    end
    _sleep(50)
end

function NGX_LandALT_inc ()
    ipc.control(69852, PMDG_inc)
    if LNDALTvar < 14000 then
        LNDALTvar = LNDALTvar + 50
        if LNDALTvar > 14000 then
            LNDALTvar = 14000
        end
    end
    ipc.control(0x148AC, LNDALTvar)
    if _MCP1() then
        DspShow("LAlt", LNDALTvar)
    else
        DspMed1("Land ALT")
        DspMed2(LNDALTvar)
    end
    _sleep(50)
end

function NGX_LandALT_incfast ()
    ipc.control(69852, PMDG_inc)
    if LNDALTvar < 14000 then
        LNDALTvar = LNDALTvar + 250
        if LNDALTvar > 14000 then
            LNDALTvar = 14000
        end
    end
    ipc.control(0x148AC, LNDALTvar)
    if _MCP1() then
        DspShow("LAlt", LNDALTvar)
    else
        DspMed1("Land ALT")
        DspMed2(LNDALTvar)
    end
    _sleep(50)
end

function NGX_LandALT_dec ()
    ipc.control(69852, PMDG_dec)
    if LNDALTvar > -1000 then
        LNDALTvar = LNDALTvar - 50
        if LNDALTvar < -1000 then
            LNDALTvar = - 1000
        end
    end
    ipc.control(0x148AC, LNDALTvar)
    if _MCP1() then
        DspShow("LAlt", LNDALTvar)
    else
        DspMed1("Land ALT")
        DspMed2(LNDALTvar)
    end
    _sleep(50)
end

function NGX_LandALT_decfast ()
    ipc.control(69852, PMDG_dec)
    if LNDALTvar > -1000 then
        LNDALTvar = LNDALTvar - 250
        if LNDALTvar < -1000 then
            LNDALTvar = - 1000
        end
    end
    ipc.control(0x148AC, LNDALTvar)
    if _MCP1() then
        DspShow("LAlt", LNDALTvar)
    else
        DspMed1("Land ALT")
        DspMed2(LNDALTvar)
    end
    _sleep(50)
end

-- ## Overhead - Cabin Cond ###############

function NGX_AIRTEMP_show ()
local txt
local pos = ipc.readLvar('ngx_switch_313_a')
    if ATEMPval == 0 then txt = "SCnt"
    elseif ATEMPval == 10 then txt = "SFwd"
    elseif ATEMPval == 20 then txt = "SAft"
    elseif ATEMPval == 30 then txt = "CFwd"
    elseif ATEMPval == 40 then txt = "CAft"
    elseif ATEMPval == 50 then txt = "PckL"
    elseif ATEMPval == 60 then txt = "PckR"
    end
    DspShow("TEMP", txt)
end

function NGX_AIRTEMP_inc ()
local pos = ipc.readLvar('ngx_switch_313_a')
    if pos < 60 then
        ipc.control(PMDGBaseVar+313, PMDG_inc)
        NGX_AIRTEMP_show ()
    end
end

function NGX_AIRTEMP_dec ()
local pos = ipc.readLvar('ngx_switch_313_a')
    if pos > 0 then
        ipc.control(PMDGBaseVar+313, PMDG_dec)
        NGX_AIRTEMP_show ()
    end
end

function NGX_AIRTEMP_calc (sel)
if sel == nil then return end
local pos = ipc.readLvar("L:ngx_switch_313_a")/10
    if pos > sel then
        for i = 1, (pos - sel), 1 do
            NGX_AIRTEMP_dec ()
            ipc.sleep(50)
        end
    elseif pos < sel then
        for i = 1, (sel - pos), 1 do
            NGX_AIRTEMP_inc ()
            ipc.sleep(50)
        end
    end
end

function NGX_AIRTEMP_cycle ()
local pos = ipc.readLvar('ngx_switch_313_a')
    if pos < 60 then
        NGX_AIRTEMP_inc()
    else
        NGX_AIRTEMP_SCnt()
    end
end

function NGX_AIRTEMP_SCnt ()
    if ipc.readLvar('ngx_switch_313_a') ~= 0 then
        NGX_AIRTEMP_calc(0)
        NGX_AIRTEMP_show ()
    end
end

function NGX_AIRTEMP_SFwd ()
    if ipc.readLvar('ngx_switch_313_a') ~= 10 then
        NGX_AIRTEMP_calc(1)
        NGX_AIRTEMP_show ()
    end
end

function NGX_AIRTEMP_SAft ()
    if ipc.readLvar('ngx_switch_313_a') ~= 20 then
        NGX_AIRTEMP_calc(2)
        NGX_AIRTEMP_show ()
    end
end

function NGX_AIRTEMP_CFwd ()
    if ipc.readLvar('ngx_switch_313_a') ~= 30 then
        NGX_AIRTEMP_calc(3)
        NGX_AIRTEMP_show ()
    end
end

function NGX_AIRTEMP_CAft ()
    if ipc.readLvar('ngx_switch_313_a') ~= 40 then
        NGX_AIRTEMP_calc(4)
        NGX_AIRTEMP_show ()
    end
end

function NGX_AIRTEMP_PckL ()
    if ipc.readLvar('ngx_switch_313_a') ~= 50 then
        NGX_AIRTEMP_calc(5)
        NGX_AIRTEMP_show ()
    end
end

function NGX_AIRTEMP_PckR ()
    if ipc.readLvar('ngx_switch_313_a') ~= 60 then
        NGX_AIRTEMP_calc(6)
        NGX_AIRTEMP_show ()
    end
end

function NGX_TRIMAIR_on ()
    if ipc.readLvar('ngx_switch_311_a') ~= 100 then
        ipc.control(PMDGBaseVar+311, PMDG_ClkL)
        DspShow ("TRIM", " on ")
    end
end

function NGX_TRIMAIR_off ()
    if ipc.readLvar('ngx_switch_311_a') ~= 0 then
        ipc.control(PMDGBaseVar+311, PMDG_ClkR)
        DspShow ("TRIM", " off")
    end
end

function NGX_TRIMAIR_toggle ()
    if ipc.readLvar('ngx_switch_311_a') == 100 then
        NGX_TRIMAIR_off()
    else
        NGX_TRIMAIR_on()
    end
end

function NGX_RECIRC_L_auto ()
    if ipc.readLvar('ngx_switch_872_a') ~= 100 then
        ipc.control(PMDGBaseVar+872, PMDG_ClkL)
        DspShow ("RCRL", "auto")
    end
end

function NGX_RECIRC_L_off ()
    if ipc.readLvar('ngx_switch_872_a') ~= 0 then
        ipc.control(PMDGBaseVar+872, PMDG_ClkR)
        DspShow ("RCRL", " off")
    end
end

function NGX_RECIRC_L_toggle ()
    if ipc.readLvar('ngx_switch_872_a') == 100 then
        NGX_RECIRC_L_off()
    else
        NGX_RECIRC_L_auto()
    end
end


function NGX_RECIRC_R_auto ()
    if ipc.readLvar('ngx_switch_196_a') ~= 100 then
        ipc.control(PMDGBaseVar+196, PMDG_ClkL)
		DspShow ("RCCR", "auto")
    end
end

function NGX_RECIRC_R_off ()
    if ipc.readLvar('ngx_switch_196_a') ~= 0 then
        ipc.control(PMDGBaseVar+196, PMDG_ClkR)
		DspShow ("RCCR", " off")
    end
end

function NGX_RECIRC_R_toggle ()
    if ipc.readLvar('ngx_switch_196_a') == 100 then
        NGX_RECIRC_R_off()
    else
        NGX_RECIRC_R_auto()
    end
end

function NGX_PACK_L_show ()
local txt
local pos = ipc.readLvar('ngx_switch_200_a')
    if pos == 0 then txt = " off"
    elseif pos == 50 then txt = "auto"
    elseif pos == 100 then txt = "high"
    end
    DspShow("PCKL", txt)
end

function NGX_PACK_L_high ()
    if ipc.readLvar('ngx_switch_200_a') ~= 100 then
        ipc.control(PMDGBaseVar+200, PMDG_ClkL)
        ipc.control(PMDGBaseVar+200, PMDG_ClkL)
		NGX_PACK_L_show ()
    end
end

function NGX_PACK_L_auto ()
local pos = ipc.readLvar('ngx_switch_200_a')
    if pos > 50 then
        ipc.control(PMDGBaseVar+200, PMDG_ClkR)
		NGX_PACK_L_show ()
    elseif pos < 50 then
        ipc.control(PMDGBaseVar+200, PMDG_ClkL)
		NGX_PACK_L_show ()
    end
end

function NGX_PACK_L_off ()
    if ipc.readLvar('ngx_switch_200_a') ~= 0 then
        ipc.control(PMDGBaseVar+200, PMDG_ClkR)
        ipc.control(PMDGBaseVar+200, PMDG_ClkR)
		NGX_PACK_L_show ()
    end
end

function NGX_PACK_L_inc ()
local pos = ipc.readLvar('ngx_switch_200_a')
    if pos < 100 then
        ipc.control(PMDGBaseVar+200, PMDG_ClkL)
		NGX_PACK_L_show ()
    end
end

function NGX_PACK_L_dec ()
local pos = ipc.readLvar('ngx_switch_200_a')
    if pos > 0 then
        ipc.control(PMDGBaseVar+200, PMDG_ClkR)
		NGX_PACK_L_show ()
    end
end

function NGX_PACK_R_show ()
local txt
local pos = ipc.readLvar('ngx_switch_201_a')
    if pos == 0 then txt = " off"
    elseif pos == 50 then txt = "auto"
    elseif pos == 100 then txt = "high"
    end
    DspShow("PCKR", txt)
end

function NGX_PACK_R_high ()
    if ipc.readLvar('ngx_switch_201_a') ~= 100 then
        ipc.control(PMDGBaseVar+201, PMDG_ClkL)
        ipc.control(PMDGBaseVar+201, PMDG_ClkL)
		NGX_PACK_L_show ()
    end
end

function NGX_PACK_R_auto ()
local pos = ipc.readLvar('ngx_switch_201_a')
    if pos > 50 then
        ipc.control(PMDGBaseVar+201, PMDG_ClkR)
		NGX_PACK_L_show ()
    elseif pos < 50 then
        ipc.control(PMDGBaseVar+201, PMDG_ClkL)
		NGX_PACK_L_show ()
    end
end

function NGX_PACK_R_off ()
    if ipc.readLvar('ngx_switch_201_a') ~= 0 then
        ipc.control(PMDGBaseVar+201, PMDG_ClkR)
        ipc.control(PMDGBaseVar+201, PMDG_ClkR)
		NGX_PACK_R_show ()
    end
end

function NGX_PACK_R_inc ()
local pos = ipc.readLvar('ngx_switch_201_a')
    if pos < 100 then
        ipc.control(PMDGBaseVar+201, PMDG_ClkL)
		NGX_PACK_R_show ()
    end
end

function NGX_PACK_R_dec ()
local pos = ipc.readLvar('ngx_switch_201_a')
    if pos > 0 then
        ipc.control(PMDGBaseVar+201, PMDG_ClkR)
		NGX_PACK_R_show ()
    end
end

function NGX_ISOL_VALVE_show ()
local txt
local pos = ipc.readLvar('ngx_switch_202_a')
    if pos == 0 then txt = " clos"
    elseif pos == 50 then txt = "auto"
    elseif pos == 100 then txt = "open"
    end
    DspShow("ISOL", txt)
end

function NGX_ISOL_VALVE_open ()
    if ipc.readLvar('ngx_switch_202_a') ~= 100 then
        ipc.control(PMDGBaseVar+202, PMDG_ClkL)
        ipc.control(PMDGBaseVar+202, PMDG_ClkL)
		NGX_ISOL_VALVE_show ()
    end
end

function NGX_ISOL_VALVE_auto ()
local pos = ipc.readLvar('ngx_switch_202_a')
    if pos > 50 then
        ipc.control(PMDGBaseVar+202, PMDG_ClkR)
		NGX_PACK_L_show ()
    elseif pos < 50 then
        ipc.control(PMDGBaseVar+202, PMDG_ClkL)
		NGX_PACK_L_show ()
    end
end

function NGX_ISOL_VALVE_off ()
    if ipc.readLvar('ngx_switch_202_a') ~= 0 then
        ipc.control(PMDGBaseVar+202, PMDG_ClkR)
        ipc.control(PMDGBaseVar+202, PMDG_ClkR)
		NGX_ISOL_VALVE_show ()
    end
end

function NGX_ISOL_VALVE_inc ()
local pos = ipc.readLvar('ngx_switch_202_a')
    if pos < 100 then
        ipc.control(PMDGBaseVar+202, PMDG_ClkL)
		NGX_ISOL_VALVE_show ()
    end
end

function NGX_ISOL_VALVE_dec ()
local pos = ipc.readLvar('ngx_switch_202_a')
    if pos >0 then
        ipc.control(PMDGBaseVar+202, PMDG_ClkR)
		NGX_ISOL_VALVE_show ()
    end
end

function NGX_BLEED1_show ()
local pos = ipc.readLvar('ngx_switch_210_a')
    if pos == 0 then txt = " off"
    elseif pos == 100 then txt = " on"
    end
    DspShow("BLE1", txt)
end

function NGX_BLEED1_on ()
    if ipc.readLvar('ngx_switch_210_a') ~= 100 then
        ipc.control(PMDGBaseVar+210, PMDG_ClkL)
		NGX_BLEED1_show ()
    end
end

function NGX_BLEED1_off ()
    if ipc.readLvar('ngx_switch_210_a') ~= 0 then
        ipc.control(PMDGBaseVar+210, PMDG_ClkR)
		NGX_BLEED1_show ()
    end
end

function NGX_BLEED1_toggle ()
    if ipc.readLvar('ngx_switch_210_a') == 100 then
        NGX_BLEED1_off()
    else
        NGX_BLEED1_on()
    end
end

function NGX_BLEED2_show ()
local pos = ipc.readLvar('ngx_switch_212_a')
    if pos == 0 then txt = " off"
    elseif pos == 100 then txt = " on"
    end
    DspShow("BLE2", txt)
end

function NGX_BLEED2_on ()
    if ipc.readLvar('ngx_switch_212_a') ~= 100 then
        ipc.control(PMDGBaseVar+212, PMDG_ClkL)
		NGX_BLEED2_show ()
    end
end

function NGX_BLEED2_off ()
    if ipc.readLvar('ngx_switch_212_a') ~= 0 then
        ipc.control(PMDGBaseVar+212, PMDG_ClkR)
		NGX_BLEED2_show ()
    end
end

function NGX_BLEED2_toggle ()
    if ipc.readLvar('ngx_switch_212_a') == 100 then
        NGX_BLEED2_off()
    else
        NGX_BLEED2_on()
    end
end

function NGX_APUBLEED_show ()
local pos = ipc.readLvar('ngx_switch_211_a')
    if pos == 0 then txt = " off"
    elseif pos == 100 then txt = " on"
    end
    DspShow("BLEa", txt)
end

function NGX_APUBLEED_on ()
    if ipc.readLvar('ngx_switch_211_a') ~= 100 then
        ipc.control(PMDGBaseVar+211, PMDG_ClkL)
		NGX_APUBLEED_show ()
    end
end

function NGX_APUBLEED_off ()
    if ipc.readLvar('ngx_switch_211_a') ~= 0 then
        ipc.control(PMDGBaseVar+211, PMDG_ClkR)
		NGX_APUBLEED_show ()
    end
end

function NGX_APUBLEED_toggle ()
    if ipc.readLvar('ngx_switch_211_a') == 100 then
        NGX_APUBLEED_off()
    else
        NGX_APUBLEED_on()
    end
end

function NGX_RecircANDIsol_auto ()
   NGX_RECIRC_L_auto ()
   _sleep(150, 350)
    NGX_RECIRC_R_auto ()
   _sleep(150, 350)
   NGX_ISOL_VALVE_auto ()
end

function NGX_RecircANDIsol_off ()
   NGX_RECIRC_L_off ()
   _sleep(150, 350)
    NGX_RECIRC_R_off ()
   _sleep(150, 350)
   NGX_ISOL_VALVE_off ()
end

-- ## Overhead - Engine start ###############

function NGX_IGN_left ()
    if ipc.readLvar('ngx_switch_120_a') ~= 0 then
        ipc.control(PMDGBaseVar+120, PMDG_ClkL)
        ipc.control(PMDGBaseVar+120, PMDG_ClkL)
		DspShow ("IGN", " L ")
    end
end

function NGX_IGN_both ()
local pos = ipc.readLvar('ngx_switch_120_a')
    if pos < 50 then
        ipc.control(PMDGBaseVar+120, PMDG_ClkR)
        DspShow ("IGN", "both")
    elseif pos > 50 then
        ipc.control(PMDGBaseVar+120, PMDG_ClkL)
        DspShow ("IGN", "both")
    end
end

function NGX_IGN_right ()
    if ipc.readLvar('ngx_switch_120_a') ~= 100 then
        ipc.control(PMDGBaseVar+120, PMDG_ClkR)
        ipc.control(PMDGBaseVar+120, PMDG_ClkR)
		DspShow ("IGN", " R ")
    end
end

-- function NGX_ENG1_toggle ()
    -- if ipc.readLvar('ngx_switch_688_a') == 100 then
        -- NGX_ENG1_idle()
    -- else
        -- NGX_ENG1_cutoff()
    -- end
-- end



-- function NGX_ENG2_toggle ()
    -- if ipc.readLvar('ngx_switch_689_a') == 100 then
        -- NGX_ENG2_idle()
    -- else
        -- NGX_ENG2_cutoff()
    -- end
-- end

--------------------

function NGX_ENG1_START_show ()
local txt
local pos = ipc.readLvar('ngx_switch_119_a')
    if pos == 0 then txt ="grd"
    elseif pos == 10 then txt = "off"
    elseif pos == 20 then txt = "cont"
    elseif pos == 30 then txt = "flt"
    end
    DspShow ("ENG1", txt)
end

function NGX_ENG1_START_inc ()
local pos = ipc.readLvar('ngx_switch_119_a')
    if pos < 30 then
        ipc.control(PMDGBaseVar+119, PMDG_inc)
		NGX_ENG1_START_show ()
    end
end

function NGX_ENG1_START_dec ()
local pos = ipc.readLvar('ngx_switch_119_a')
    if pos > 0 then
        ipc.control(PMDGBaseVar+119, PMDG_dec)
		NGX_ENG1_START_show ()
    end
end

function NGX_ENG1_START_calc (sel)
if sel == nil then return end
local pos = ipc.readLvar("L:ngx_switch_119_a")/10
    if pos > sel then
        for i = 1, (pos - sel), 1 do
            NGX_ENG1_START_dec ()
            ipc.sleep(50)
        end
    elseif pos < sel then
        for i = 1, (sel - pos), 1 do
            NGX_ENG1_START_inc ()
            ipc.sleep(50)
        end
    end
end

function NGX_ENG1_START_GRD ()
local pos = ipc.readLvar('ngx_switch_119_a')
    if pos ~= 0 then
        NGX_ENG1_START_calc(0)
        NGX_ENG1_START_show ()
    end
end

function NGX_ENG1_START_OFF ()
local pos = ipc.readLvar('ngx_switch_119_a')
    if pos ~= 10 then
        NGX_ENG1_START_calc(1)
        NGX_ENG1_START_show ()
    end
end

function NGX_ENG1_START_CONT ()
local pos = ipc.readLvar('ngx_switch_119_a')
    if pos ~= 20 then
        NGX_ENG1_START_calc(2)
        NGX_ENG1_START_show ()
    end
end

function NGX_ENG1_START_FLT ()
local pos = ipc.readLvar('ngx_switch_119_a')
    if pos ~= 30 then
        NGX_ENG1_START_calc(3)
        NGX_ENG1_START_show ()
    end
end

function NGX_ENG1_START_cycle ()
local pos = ipc.readLvar('ngx_switch_119_a')
    if pos < 30 then
        NGX_ENG1_START_inc()
    else
        NGX_ENG1_START_GRD()
    end
end

---

function NGX_ENG2_START_show ()
local txt
local pos = ipc.readLvar('ngx_switch_121_a')
    if pos == 0 then txt ="grd"
    elseif pos == 10 then txt = "off"
    elseif pos == 20 then txt = "cont"
    elseif pos == 30 then txt = "flt"
    end
    DspShow ("ENG1", txt)
end

function NGX_ENG2_START_inc ()
local pos = ipc.readLvar('ngx_switch_121_a')
    if pos < 30 then
        ipc.control(PMDGBaseVar+121, PMDG_inc)
		NGX_ENG1_START_show ()
    end
end

function NGX_ENG2_START_dec ()
local pos = ipc.readLvar('ngx_switch_121_a')
    if pos > 0 then
        ipc.control(PMDGBaseVar+121, PMDG_dec)
		NGX_ENG1_START_show ()
    end
end

function NGX_ENG2_START_calc (sel)
if sel == nil then return end
local pos = ipc.readLvar("L:ngx_switch_121_a")/10
    if pos > sel then
        for i = 1, (pos - sel), 1 do
            NGX_ENG2_START_dec ()
            ipc.sleep(50)
        end
    elseif pos < sel then
        for i = 1, (sel - pos), 1 do
            NGX_ENG2_START_inc ()
            ipc.sleep(50)
        end
    end
end

function NGX_ENG2_START_GRD ()
local pos = ipc.readLvar('ngx_switch_121_a')
    if pos ~= 0 then
        NGX_ENG2_START_calc(0)
        NGX_ENG2_START_show ()
    end
end

function NGX_ENG2_START_OFF ()
local pos = ipc.readLvar('ngx_switch_121_a')
    if pos ~= 10 then
        NGX_ENG2_START_calc(1)
        NGX_ENG2_START_show ()
    end
end

function NGX_ENG2_START_CONT ()
local pos = ipc.readLvar('ngx_switch_121_a')
    if pos ~= 20 then
        NGX_ENG2_START_calc(2)
        NGX_ENG2_START_show ()
    end
end

function NGX_ENG2_START_FLT ()
local pos = ipc.readLvar('ngx_switch_121_a')
    if pos ~= 30 then
        NGX_ENG2_START_calc(3)
        NGX_ENG2_START_show ()
    end
end

function NGX_ENG2_START_cycle ()
local pos = ipc.readLvar('ngx_switch_121_a')
    if pos < 30 then
        NGX_ENG2_START_inc()
    else
        NGX_ENG2_START_GRD()
    end
end

-- ## Lights - External ###############

-- $$ Landing Lights

function NGX_LAND_FIXED_R_on ()
    if ipc.readLvar('ngx_switch_114_a') ~= 100 then
        ipc.control(PMDGBaseVar+114, PMDG_ClkL)
		DspShow ("LLgt", "R on", "LandLght", "Right on")
    end
end

function NGX_LAND_FIXED_R_off ()
    if ipc.readLvar('ngx_switch_114_a') ~= 0 then
        ipc.control(PMDGBaseVar+114, PMDG_ClkR)
		DspShow ("LLgt", "Roff", "LandLght", "Rght off")
    end
end

function NGX_LAND_FIXED_L_on ()
    if ipc.readLvar('ngx_switch_113_a') ~= 100 then
        ipc.control(PMDGBaseVar+113, PMDG_ClkL)
		DspShow ("LLgt", "L on", "LandLght", "Left on")
    end
end

function NGX_LAND_FIXED_L_off ()
    if ipc.readLvar('ngx_switch_113_a') ~= 0 then
        ipc.control(PMDGBaseVar+113, PMDG_ClkR)
		DspShow ("LLgt", "Loff", "LandLght", "Left off")
    end
end

function NGX_LAND_FIXED_BOTH_on ()
    NGX_LAND_FIXED_L_on ()
    _sleep(10,20)
    NGX_LAND_FIXED_R_on ()
end

function NGX_LAND_FIXED_BOTH_off ()
    NGX_LAND_FIXED_L_off ()
    _sleep(10,20)
    NGX_LAND_FIXED_R_off ()
end

function NGX_LAND_RETR_R_on ()
    if ipc.readLvar('ngx_switch_112_a') ~= 100 then
        ipc.control(PMDGBaseVar+112, PMDG_ClkL)
        ipc.control(PMDGBaseVar+112, PMDG_ClkL)
		DspShow ("RLgt", "R on", "RetrLght", "Right on")
    end
end

function NGX_LAND_RETR_R_extend ()
local pos = ipc.readLvar('ngx_switch_112_a')
    if pos > 50 then
        ipc.control(PMDGBaseVar+112, PMDG_ClkR)
		DspShow ("RLgt", "Rext", "RetrLght", "Rght ext")
    elseif pos < 50 then
        ipc.control(PMDGBaseVar+112, PMDG_ClkL)
		DspShow ("RLgt", "Rext", "RetrLght", "Rght ext")
    end
end

function NGX_LAND_RETR_R_off ()
    if ipc.readLvar('ngx_switch_112_a') ~= 0 then
        ipc.control(PMDGBaseVar+112, PMDG_ClkR)
        ipc.control(PMDGBaseVar+112, PMDG_ClkR)
		DspShow ("RLgt", "Roff", "RetrLght", "Rght off")
    end
end

function NGX_LAND_RETR_L_on ()
    if ipc.readLvar('ngx_switch_111_a') ~= 100 then
        ipc.control(PMDGBaseVar+111, PMDG_ClkL)
        ipc.control(PMDGBaseVar+111, PMDG_ClkL)
		DspShow ("RLgt", "L on", "RetrLght", "Left on")
    end
end

function NGX_LAND_RETR_L_extend ()
local pos = ipc.readLvar('ngx_switch_111_a')
    if pos > 50 then
        ipc.control(PMDGBaseVar+111, PMDG_ClkR)
		DspShow ("RLgt", "Lext", "RetrLght", "left ext")
    elseif pos < 50 then
        ipc.control(PMDGBaseVar+111, PMDG_ClkL)
		DspShow ("RLgt", "Lext", "RetrLght", "left ext")
    end
end

function NGX_LAND_RETR_L_off ()
    if ipc.readLvar('ngx_switch_111_a') ~= 0 then
        ipc.control(PMDGBaseVar+111, PMDG_ClkR)
        ipc.control(PMDGBaseVar+111, PMDG_ClkR)
		DspShow ("RLgt", "Loff", "RetrLght", "Left off")
    end
end

function NGX_LAND_RETR_BOTH_on ()
    NGX_LAND_RETR_L_on ()
    _sleep(10,20)
    NGX_LAND_RETR_R_on ()
end

function NGX_LAND_RETR_BOTH_extend ()
    NGX_LAND_RETR_L_extend ()
    _sleep(10,20)
    NGX_LAND_RETR_R_extend ()
end

function NGX_LAND_RETR_BOTH_off ()
    NGX_LAND_RETR_L_off ()
    _sleep(10,20)
    NGX_LAND_RETR_R_off ()
end

function NGX_LAND_ALL_on ()
    DspShow ("LAND", "on")
    ipc.control(PMDGBaseVar+110, PMDG_ClkL)
    NGX_LAND_RETR_L_on ()
    NGX_LAND_RETR_R_on ()
    NGX_LAND_FIXED_L_on ()
    NGX_LAND_FIXED_R_on ()
end

function NGX_LAND_ALL_off ()
    --DspShow ("LAND", "OFF")
    NGX_LAND_RETR_L_off ()
    NGX_LAND_RETR_R_off ()
    NGX_LAND_FIXED_L_off ()
    NGX_LAND_FIXED_R_off ()
end

function NGX_LAND_ALL_toggle ()
    if _t('LAND_ALL') then
        NGX_LAND_ALL_on ()
    else
        NGX_LAND_ALL_off ()
    end
end

-- $$ Turn Off Lights

function NGX_TURNOFF_LEFT_on ()
    if ipc.readLvar('ngx_switch_115_a') ~= 100 then
        ipc.control(PMDGBaseVar+115, PMDG_ClkL)
		DspShow ("TOff", "L on", "TURNOFF", "left on")
    end
end

function NGX_TURNOFF_LEFT_off ()
    if ipc.readLvar('ngx_switch_115_a') ~= 0 then
        ipc.control(PMDGBaseVar+115, PMDG_ClkL)
		DspShow ("TOff", "Loff", "TURNOFF", "left off")
    end
end

function NGX_TURNOFF_LEFT_toggle ()
    if ipc.readLvar('ngx_switch_115_a') == 0 then
        NGX_TURNOFF_LEFT_on()
    else
        NGX_TURNOFF_LEFT_off()
    end
end

function NGX_TURNOFF_RIGHT_on ()
    if ipc.readLvar('ngx_switch_116_a') ~= 100 then
        ipc.control(PMDGBaseVar+116, PMDG_ClkL)
		DspShow ("TOff", "R on", "TURNOFF", "right on")
    end
end

function NGX_TURNOFF_RIGHT_off ()
    if ipc.readLvar('ngx_switch_116_a') ~= 0 then
        ipc.control(PMDGBaseVar+116, PMDG_ClkL)
		DspShow ("TOff", "Roff", "TURNOFF", "rght off")
    end
end

function NGX_TURNOFF_RIGHT_toggle ()
    if ipc.readLvar('ngx_switch_116_a') == 0 then
        NGX_TURNOFF_RIGHT_on()
    else
        NGX_TURNOFF_RIGHT_off()
    end
end

function NGX_TURNOFF_BOTH_on ()
    NGX_TURNOFF_LEFT_on ()
    --_sleep(10,20)
    NGX_TURNOFF_RIGHT_on ()
end

function NGX_TURNOFF_BOTH_off ()
    NGX_TURNOFF_LEFT_off ()
    --_sleep(10,20)
    NGX_TURNOFF_RIGHT_off ()
end

-- $$ Taxi Lights

function NGX_TAXI_on ()
    if ipc.readLvar('ngx_switch_117_a') ~= 100 then
        ipc.control(PMDGBaseVar+117, PMDG_ClkL)
		DspShow ("TAXI", "on", "TAXI", "on")
    end
end

function NGX_TAXI_off ()
    if ipc.readLvar('ngx_switch_117_a') ~= 0 then
        ipc.control(PMDGBaseVar+117, PMDG_ClkL)
		DspShow ("TAXI", "off", "TAXI", "off")
    end
end

function NGX_TAXI_toggle ()
    if ipc.readLvar('ngx_switch_117_a') == 0 then
        NGX_TAXI_on()
    else
        NGX_TAXI_off()
    end
end

function NGX_TAXI_ALL_on ()
    NGX_TURNOFF_LEFT_on ()
    --_sleep(10, 20)
    NGX_TURNOFF_RIGHT_on ()
    --_sleep(10, 20)
    NGX_TAXI_on ()
    DspShow ("TAXI", "on")
end

function NGX_TAXI_ALL_off ()
    NGX_TURNOFF_LEFT_off ()
    --_sleep(10, 20)
    NGX_TURNOFF_RIGHT_off ()
    --_sleep(10, 20)
    NGX_TAXI_off ()
    DspShow ("TAXI", "off")
end

function NGX_TAXI_ALL_toggle ()
    if ipc.readLvar('ngx_switch_117_a') == 0 then
        NGX_TAXI_ALL_on ()
    else
        NGX_TAXI_ALL_off ()
    end
end

function NGX_TURNOFF_BOTH_toggle ()
    NGX_TURNOFF_LEFT_toggle ()
    --_sleep(10, 20)
    NGX_TURNOFF_RIGHT_toggle ()
end

-- $$ Logo Lights

function NGX_LOGO_on ()
    if ipc.readLvar('ngx_switch_122_a') ~= 100 then
        ipc.control(PMDGBaseVar+122, PMDG_ClkL)
		DspShow ("LOGO", "on ")
    end
end

function NGX_LOGO_off ()
    if ipc.readLvar('ngx_switch_122_a') ~= 0 then
        ipc.control(PMDGBaseVar+122, PMDG_ClkR)
		DspShow ("LOGO", "off")
    end
end

function NGX_LOGO_toggle ()
    if ipc.readLvar('ngx_switch_122_a') == 0 then
        NGX_LOGO_on ()
    else
        NGX_LOGO_off ()
    end
end

-- $$ Navigation Lights

function NGX_NAV_strobe ()
    if ipc.readLvar('ngx_switch_123_a') ~= 0 then
        ipc.control(PMDGBaseVar+123, PMDG_ClkR)
        ipc.control(PMDGBaseVar+123, PMDG_ClkR)
		DspShow ("NAV ", "strb")
    end
end

function NGX_NAV_off ()
local pos =ipc.readLvar('ngx_switch_123_a')
    if pos > 50 then
        ipc.control(PMDGBaseVar+123, PMDG_ClkR)
		DspShow ("NAV ", "off ")
    elseif pos < 50 then
        ipc.control(PMDGBaseVar+123, PMDG_ClkL)
		DspShow ("NAV ", "off ")
    end
end

function NGX_NAV_steady ()
    if ipc.readLvar('ngx_switch_123_a') ~= 100 then
        ipc.control(PMDGBaseVar+123, PMDG_ClkL)
        ipc.control(PMDGBaseVar+123, PMDG_ClkL)
		DspShow ("NAV ", "stdy")
    end
end


-- $$ Beacon Lights

function NGX_BEACON_on ()
    if ipc.readLvar('ngx_switch_124_a') ~= 100 then
        ipc.control(PMDGBaseVar+124, PMDG_ClkR)
		DspShow ("BCN ", "on")
    end
end

function NGX_BEACON_off ()
    if ipc.readLvar('ngx_switch_124_a') ~= 0 then
        ipc.control(PMDGBaseVar+124, PMDG_ClkL)
		DspShow ("BCN ", "off")
    end
end

function NGX_BEACON_toggle ()
    if ipc.readLvar('ngx_switch_124_a') == 0 then
        NGX_BEACON_on ()
    else
        NGX_BEACON_off ()
    end
end

-- $$ Wing Lights

function NGX_WING_on ()
    if ipc.readLvar('ngx_switch_125_a') ~= 100 then
        ipc.control(PMDGBaseVar+125, PMDG_ClkR)
		DspShow ("WING", "on ")
    end
end

function NGX_WING_off ()
    if ipc.readLvar('ngx_switch_125_a') ~= 0 then
        ipc.control(PMDGBaseVar+125, PMDG_ClkL)
		DspShow ("WING", "off")
    end
end

function NGX_WING_toggle ()
    if ipc.readLvar('ngx_switch_125_a') == 0 then
        NGX_WING_on ()
    else
        NGX_WING_off ()
    end
end

-- $$ Wheel Well Lights

function NGX_WHEEL_WELL_on ()
    if ipc.readLvar('ngx_switch_126_a') ~= 100 then
        ipc.control(PMDGBaseVar+126, PMDG_ClkR)
		DspShow ("WELL", " on ")
    end
end

function NGX_WHEEL_WELL_off ()
    if ipc.readLvar('ngx_switch_126_a') ~= 0 then
        ipc.control(PMDGBaseVar+126, PMDG_ClkL)
		DspShow ("WELL", " on ")
    end
end

function NGX_WHEEL_WELL_toggle ()
    if ipc.readLvar('ngx_switch_126_a') == 0 then
        NGX_WHEEL_WELL_on ()
    else
        NGX_WHEEL_WELL_off ()
    end
end

-- $$ Logo and Navigation Lights

function NGX_LOGO_and_NAV_steady ()
    NGX_LOGO_on ()
    NGX_NAV_steady ()
end

function NGX_LOGO_and_NAV_off ()
    NGX_LOGO_off ()
    NGX_NAV_off ()
end

-- ## Lights - Cockpit ###############

-- $$ Emergency Lights

function NGX_EMER_lights_off ()
    if true then --ipc.readLvar('ngx_switch_100_a') ~= 0 then
        if ipc.readLvar('ngx_switch_101_a') < 100 then
            ipc.control(PMDGBaseVar+101, PMDG_ClkR)
            ipc.sleep(50)
        end
        ipc.sleep(50)
        ipc.control(PMDGBaseVar+100, PMDG_ClkR)
        ipc.control(PMDGBaseVar+100, PMDG_ClkR)
        DspShow("EMEG", "off")
    end
end

function NGX_EMER_lights_armed ()
local pos = ipc.readLvar('ngx_switch_100_a')
    if pos > 50 then
        if ipc.readLvar('ngx_switch_101_a') < 100 then
            ipc.control(PMDGBaseVar+101, PMDG_ClkR)
            ipc.sleep(50)
        end
        ipc.control(PMDGBaseVar+100, PMDG_ClkR)
        DspShow("EMEG", "armd")
    elseif pos < 50 then
        if ipc.readLvar('ngx_switch_101_a') < 100 then
            ipc.control(PMDGBaseVar+101, PMDG_ClkR)
            ipc.sleep(50)
        end
        ipc.control(PMDGBaseVar+100, PMDG_ClkL)
        DspShow("EMEG", "armd")
    end
    if ipc.readLvar('ngx_switch_101_a') == 100 then
        ipc.sleep(250)
        ipc.control(PMDGBaseVar+101, PMDG_ClkL)
    end
end

function NGX_EMER_lights_on ()
    if ipc.readLvar('ngx_switch_100_a') ~= 100 then
        if ipc.readLvar('ngx_switch_101_a') < 100 then
            ipc.control(PMDGBaseVar+101, PMDG_ClkR)
            ipc.sleep(50)
        end
        ipc.control(PMDGBaseVar+100, PMDG_ClkL)
        ipc.control(PMDGBaseVar+100, PMDG_ClkL)
        DspShow("EMEG", "on")
    end
end

-- $$ Dome Lights

function NGX_DOME_dim ()
    if ipc.readLvar('ngx_switch_258_a') ~= 0 then
        ipc.control(PMDGBaseVar+258, PMDG_ClkR)
        ipc.control(PMDGBaseVar+258, PMDG_ClkR)
		DspShow ("DOME", " dim")
    end
end

function NGX_DOME_off ()
local pos =ipc.readLvar('ngx_switch_258_a')
    if pos > 50 then
        ipc.control(PMDGBaseVar+258, PMDG_ClkR)
		DspShow ("DOME", "off")
    elseif pos < 50 then
        ipc.control(PMDGBaseVar+258, PMDG_ClkL)
		DspShow ("DOME", "off")
    end
end

function NGX_DOME_bright ()
    if ipc.readLvar('ngx_switch_258_a') ~= 100 then
        ipc.control(PMDGBaseVar+258, PMDG_ClkL)
        ipc.control(PMDGBaseVar+258, PMDG_ClkL)
        DspShow ("DOME", " brt")
    end
end

function NGX_DOME_cycle ()
local pos =ipc.readLvar('ngx_switch_258_a')
    if pos == 100 then
        NGX_DOME_off ()
    elseif pos == 50 then
        NGX_DOME_dim ()
    elseif pos == 0 then
        NGX_DOME_bright ()
    end
end

function NGX_DOME_toggle ()
    if ipc.readLvar('ngx_switch_258_a') == 100 then
        NGX_DOME_off ()
    else
        NGX_DOME_bright ()
    end
end

-- $$ Flood and Panel Lights

function NGX_FLOOD_PDST_inc ()
    ipc.control(PMDGBaseVar+756, PMDG_inc)
end

function NGX_FLOOD_PDST_dec ()
    ipc.control(PMDGBaseVar+756, PMDG_dec)
end

function NGX_FLOOD_PANEL_inc ()
    ipc.control(PMDGBaseVar+337, PMDG_inc)
end

function NGX_FLOOD_PANEL_dec ()
    ipc.control(PMDGBaseVar+337, PMDG_dec)
end

function NGX_FLOOD_MCP_inc ()
    ipc.control(PMDGBaseVar+338, PMDG_inc)
end

function NGX_FLOOD_MCP_dec ()
    ipc.control(PMDGBaseVar+338, PMDG_dec)
end

function NGX_LIGHT_OVH_inc ()
    ipc.control(PMDGBaseVar+95, PMDG_inc)
end

function NGX_LIGHT_OVH_dec ()
    ipc.control(PMDGBaseVar+95, PMDG_dec)
end

function NGX_LIGHT_CB_inc ()
    ipc.control(PMDGBaseVar+94, PMDG_inc)
end

function NGX_LIGHT_CB_dec ()
    ipc.control(PMDGBaseVar+94, PMDG_dec)
end

function NGX_LIGHT_PDST_inc ()
    ipc.control(PMDGBaseVar+757, PMDG_inc)
end

function NGX_LIGHT_PDST_dec ()
    ipc.control(PMDGBaseVar+757, PMDG_dec)
end

function NGX_LIGHT_PANEL_L_inc ()
    ipc.control(PMDGBaseVar+328, PMDG_inc)
end

function NGX_LIGHT_PANEL_L_dec ()
    ipc.control(PMDGBaseVar+328, PMDG_dec)
end

function NGX_LIGHT_PANEL_R_inc ()
    ipc.control(PMDGBaseVar+510, PMDG_inc)
end

function NGX_LIGHT_PANEL_R_dec ()
    ipc.control(PMDGBaseVar+510, PMDG_dec)
end

function NGX_LIGHT_ALL_show ()
    local brt = ipc.readLvar('ngx_switch_328_a')
    brt = round(brt / 3)
    DspShow ("PANL", tostring(brt) .. '%')
end

-- $$ Multiple Lights

function NGX_LIGHT_ALL_inc ()
    local i
    for i = 1, 5 do
        NGX_LIGHT_PANEL_R_inc ()
        NGX_LIGHT_PANEL_L_inc ()
        NGX_LIGHT_PDST_inc ()
        NGX_LIGHT_OVH_inc ()
        NGX_LIGHT_CB_inc ()
    end
    NGX_LIGHT_ALL_show ()
end

function NGX_LIGHT_ALL_incfast ()
    local i
    for i = 1, 20 do
        NGX_LIGHT_PANEL_R_inc ()
        NGX_LIGHT_PANEL_L_inc ()
        NGX_LIGHT_PDST_inc ()
        NGX_LIGHT_OVH_inc ()
        NGX_LIGHT_CB_inc ()
    end
    NGX_LIGHT_ALL_show ()
end

function NGX_LIGHT_ALL_dec ()
    local i
    for i = 1, 5 do
        NGX_LIGHT_PANEL_R_dec ()
        NGX_LIGHT_PANEL_L_dec ()
        NGX_LIGHT_PDST_dec ()
        NGX_LIGHT_OVH_dec ()
        NGX_LIGHT_CB_dec ()
    end
    NGX_LIGHT_ALL_show ()
end

function NGX_LIGHT_ALL_decfast ()
    local i
    for i = 1, 20 do
        NGX_LIGHT_PANEL_R_dec ()
        NGX_LIGHT_PANEL_L_dec ()
        NGX_LIGHT_PDST_dec ()
        NGX_LIGHT_OVH_dec ()
        NGX_LIGHT_CB_dec ()
    end
    NGX_LIGHT_ALL_show ()
end

function NGX_LIGHT_ALL_on ()
    local i
    for i = 1, 100 do
        NGX_LIGHT_PANEL_R_inc ()
        NGX_LIGHT_PANEL_L_inc ()
        NGX_LIGHT_PDST_inc ()
        NGX_LIGHT_OVH_inc ()
        NGX_LIGHT_CB_inc ()
    end
    NGX_LIGHT_ALL_show ()
end

function NGX_LIGHT_ALL_off ()
    local i
    for i = 1, 100 do
        NGX_LIGHT_PANEL_R_dec ()
        NGX_LIGHT_PANEL_L_dec ()
        NGX_LIGHT_PDST_dec ()
        NGX_LIGHT_OVH_dec ()
        NGX_LIGHT_CB_dec ()
    end
    NGX_LIGHT_ALL_show ()
end

---------------------

function NGX_FLOOD_ALL_show ()
    local brt = ipc.readLvar('ngx_switch_337_a')
    brt = round(brt / 3)
    DspShow ("FLOD", tostring(brt) .. '%')
end

function NGX_FLOOD_ALL_inc ()
    local i
    for i = 1, 5 do
        NGX_FLOOD_MCP_inc ()
        NGX_FLOOD_PDST_inc ()
        NGX_FLOOD_PANEL_inc ()
    end
    NGX_FLOOD_ALL_show ()
end

function NGX_FLOOD_ALL_incfast ()
    local i
    for i = 1, 20 do
        NGX_FLOOD_MCP_inc ()
        NGX_FLOOD_PDST_inc ()
        NGX_FLOOD_PANEL_inc ()
    end
    NGX_FLOOD_ALL_show ()
end

function NGX_FLOOD_ALL_dec ()
    local i
    for i = 1, 5 do
        NGX_FLOOD_MCP_dec ()
        NGX_FLOOD_PDST_dec ()
        NGX_FLOOD_PANEL_dec ()
    end
    NGX_FLOOD_ALL_show ()
end

function NGX_FLOOD_ALL_decfast ()
    local i
    for i = 1, 20 do
        NGX_FLOOD_MCP_dec ()
        NGX_FLOOD_PDST_dec ()
        NGX_FLOOD_PANEL_dec ()
    end
    NGX_FLOOD_ALL_show ()
end

function NGX_FLOOD_ALL_on ()
    NGX_FLOOD_ALL_off ()
    local i
    for i = 1, 60 do
        NGX_FLOOD_MCP_inc ()
        NGX_FLOOD_PDST_inc ()
        NGX_FLOOD_PANEL_inc ()
    end
    NGX_FLOOD_ALL_show ()
end

function NGX_FLOOD_ALL_off ()
    local i
    for i = 1, 100 do
        NGX_FLOOD_MCP_dec ()
        NGX_FLOOD_PDST_dec ()
        NGX_FLOOD_PANEL_dec ()
    end
    NGX_FLOOD_ALL_show ()
end

function NGX_LIGHT_PANEL_ALL_toggle ()
    local i
  if ipc.readLvar('ngx_switch_757_a') == 300 then
    for i = 1, 400 do
        NGX_LIGHT_PANEL_R_dec ()
        NGX_LIGHT_PANEL_L_dec ()
        NGX_LIGHT_PDST_dec ()
        NGX_LIGHT_OVH_dec ()
        NGX_LIGHT_CB_dec ()
    end
    NGX_LIGHT_ALL_show ()
  else
    for i = 1, 400 do
        NGX_LIGHT_PANEL_R_inc ()
        NGX_LIGHT_PANEL_L_inc ()
        NGX_LIGHT_PDST_inc ()
        NGX_LIGHT_OVH_inc ()
        NGX_LIGHT_CB_inc ()
    end
    NGX_LIGHT_ALL_show ()
  end
end

-- ## Display - Brightness ###############

function NGX_DU1_INBD_BRT_inc ()
    ipc.control(69962, PMDG_inc)
end

function NGX_DU1_INBD_BRT_dec ()
    ipc.control(69962, PMDG_dec)
end

function NGX_DU1_OUTBD_BRT_inc ()
    ipc.control(69961, PMDG_inc)
end

function NGX_DU1_OUTBD_BRT_dec ()
    ipc.control(69961, PMDG_dec)
end

function NGX_DU_UPPER_BRT_inc ()
    ipc.control(69966, PMDG_inc)
end

function NGX_DU_UPPER_BRT_dec ()
    ipc.control(69966, PMDG_dec)
end

function NGX_DU_LOWER_BRT_inc ()
    ipc.control(69964, PMDG_inc)
end

function NGX_DU_LOWER_BRT_dec ()
    ipc.control(69964, PMDG_dec)
end

function NGX_DU2_INBD_BRT_inc ()
    ipc.control(70141, PMDG_inc)
end

function NGX_DU2_INBD_BRT_dec ()
    ipc.control(70141, PMDG_dec)
end

function NGX_DU2_OUTBD_BRT_inc ()
    ipc.control(70139, PMDG_inc)
end

function NGX_DU2_OUTBD_BRT_dec ()
    ipc.control(70139, PMDG_dec)
end

function NGX_DU_LEFT_BRT_inc ()
    NGX_DU1_INBD_BRT_inc ()
    NGX_DU1_OUTBD_BRT_inc ()
    DspShow ("DU-L", "BRT+")
end

function NGX_DU_LEFT_BRT_incfast ()
    local i
    for i = 1, 5 do
        NGX_DU1_INBD_BRT_inc ()
        NGX_DU1_OUTBD_BRT_inc ()
    end
    DspShow ("DU-L", "BRT+")
end

function NGX_DU_LEFT_BRT_dec ()
    NGX_DU1_INBD_BRT_dec ()
    NGX_DU1_OUTBD_BRT_dec ()
    DspShow ("DU-L", "BRT-")
end

function NGX_DU_LEFT_BRT_decfast ()
    local i
    for i = 1, 5 do
        NGX_DU1_INBD_BRT_dec ()
        NGX_DU1_OUTBD_BRT_dec ()
    end
    DspShow ("DU-L", "BRT-")
end

function NGX_DU_CENTER_inc ()
    NGX_DU_UPPER_BRT_inc ()
    NGX_DU_LOWER_BRT_inc ()
    DspShow ("DU-C", "BRT+")
end

function NGX_DU_CENTER_incfast ()
    local i
    for i = 1, 5 do
        NGX_DU_UPPER_BRT_inc ()
        NGX_DU_LOWER_BRT_inc ()
    end
    DspShow ("DU-C", "BRT+")
end

function NGX_DU_CENTER_dec ()
    NGX_DU_UPPER_BRT_dec ()
    NGX_DU_LOWER_BRT_dec ()
    DspShow ("DU-C", "BRT-")
end

function NGX_DU_CENTER_decfast ()
    local i
    for i = 1, 5 do
        NGX_DU_UPPER_BRT_dec ()
        NGX_DU_LOWER_BRT_dec ()
    end
    DspShow ("DU-C", "BRT-")
end

function NGX_DU_RIGHT_BRT_inc ()
    NGX_DU2_INBD_BRT_inc ()
    NGX_DU2_OUTBD_BRT_inc ()
    DspShow ("DU-R", "BRT+")
end

function NGX_DU_RIGHT_BRT_incfast ()
    local i
    for i = 1, 5 do
        NGX_DU2_INBD_BRT_inc ()
        NGX_DU2_OUTBD_BRT_inc ()
    end
    DspShow ("DU-R", "BRT+")
end

function NGX_DU_RIGHT_BRT_dec ()
    NGX_DU2_INBD_BRT_dec ()
    NGX_DU2_OUTBD_BRT_dec ()
    DspShow ("DU-R", "BRT-")
end

function NGX_DU_RIGHT_BRT_decfast ()
    local i
    for i = 1, 5 do
        NGX_DU2_INBD_BRT_dec ()
        NGX_DU2_OUTBD_BRT_dec ()
    end
    DspShow ("DU-R", "BRT-")
end

function NGX_DU_LR_BRT_inc ()
    NGX_DU1_INBD_BRT_inc ()
    NGX_DU1_OUTBD_BRT_inc ()
    NGX_DU2_INBD_BRT_inc ()
    NGX_DU2_OUTBD_BRT_inc ()
    DspShow ("DU", "BRT+")
end

function NGX_DU_LR_BRT_incfast ()
    local i
    for i = 1, 5 do
        NGX_DU1_INBD_BRT_inc ()
        NGX_DU1_OUTBD_BRT_inc ()
        NGX_DU2_INBD_BRT_inc ()
        NGX_DU2_OUTBD_BRT_inc ()
    end
    DspShow ("DU", "BRT+")
end

function NGX_DU_LR_BRT_dec ()
    NGX_DU1_INBD_BRT_dec ()
    NGX_DU1_OUTBD_BRT_dec ()
    NGX_DU2_INBD_BRT_dec ()
    NGX_DU2_OUTBD_BRT_dec ()
    DspShow ("DU", "BRT-")
end

function NGX_DU_LR_BRT_decfast ()
    local i
    for i = 1, 5 do
        NGX_DU1_INBD_BRT_dec ()
        NGX_DU1_OUTBD_BRT_dec ()
        NGX_DU2_INBD_BRT_dec ()
        NGX_DU2_OUTBD_BRT_dec ()
    end
    DspShow ("DU", "BRT-")
end

function NGX_DU_ALL_BRT_show ()
    local brt = ipc.readLvar('ngx_switch_329_a')
    brt = round(brt / 3)
    DspShow ("DU", tostring(brt) .. '%')
end

function NGX_DU_ALL_BRT_inc ()
    NGX_DU1_INBD_BRT_inc ()
    NGX_DU1_OUTBD_BRT_inc ()
    NGX_DU_UPPER_BRT_inc ()
    NGX_DU_LOWER_BRT_inc ()
    NGX_DU2_INBD_BRT_inc ()
    NGX_DU2_OUTBD_BRT_inc ()
    NGX_DU_ALL_BRT_show ()
end

function NGX_DU_ALL_BRT_incfast ()
    local i
    for i = 1, 5 do
        NGX_DU1_INBD_BRT_inc ()
        NGX_DU1_OUTBD_BRT_inc ()
        NGX_DU_UPPER_BRT_inc ()
        NGX_DU_LOWER_BRT_inc ()
        NGX_DU2_INBD_BRT_inc ()
        NGX_DU2_OUTBD_BRT_inc ()
    end
    NGX_DU_ALL_BRT_show ()
end

function NGX_DU_ALL_BRT_dec ()
    NGX_DU1_INBD_BRT_dec ()
    NGX_DU1_OUTBD_BRT_dec ()
    NGX_DU_UPPER_BRT_dec ()
    NGX_DU_LOWER_BRT_dec ()
    NGX_DU2_INBD_BRT_dec ()
    NGX_DU2_OUTBD_BRT_dec ()
    NGX_DU_ALL_BRT_show ()
end

function NGX_DU_ALL_BRT_decfast ()
    local i
    for i = 1, 5 do
        NGX_DU1_INBD_BRT_dec ()
        NGX_DU1_OUTBD_BRT_dec ()
        NGX_DU_UPPER_BRT_dec ()
        NGX_DU_LOWER_BRT_dec ()
        NGX_DU2_INBD_BRT_dec ()
        NGX_DU2_OUTBD_BRT_dec ()
    end
    NGX_DU_ALL_BRT_show ()
end

-- ## Displays - Modes DU1 ###############

function NGX_DU1_MAIN_show ()
local txt
local pos = ipc.readLvar('ngx_switch_335_a')
    if pos == 0 then txt ="oPFD"
    elseif pos == 10 then txt = "NORM"
    elseif pos == 20 then txt = "iENG"
    elseif pos == 30 then txt = "iPFD"
    elseif pos == 40 then txt = "iMFD"
    end
    DspShow ("DUmn", txt)
end

function NGX_DU1_MAIN_oPFD ()
    ipc.control(PMDGBaseVar+335, 0)
    ipc.sleep(50)
    NGX_DU1_MAIN_show ()
end

function NGX_DU1_MAIN_norm ()
    ipc.control(PMDGBaseVar+335, 1)
    ipc.sleep(50)
    NGX_DU1_MAIN_show ()
end

function NGX_DU1_MAIN_iENG ()
    ipc.control(PMDGBaseVar+335, 2)
    ipc.sleep(50)
    NGX_DU1_MAIN_show ()
end

function NGX_DU1_MAIN_iPFD ()
    ipc.control(PMDGBaseVar+335, 3)
    ipc.sleep(50)
    NGX_DU1_MAIN_show ()
end

function NGX_DU1_MAIN_iMFD ()
    ipc.control(PMDGBaseVar+335, 4)
    ipc.sleep(50)
    NGX_DU1_MAIN_show ()
end

function NGX_DU1_MAIN_inc ()
    ipc.control(PMDGBaseVar+335, PMDG_ClkR)
    NGX_DU1_MAIN_show ()
end

function NGX_DU1_MAIN_dec ()
    ipc.control(PMDGBaseVar+335, PMDG_ClkL)
    NGX_DU1_MAIN_show ()
end

---- lower DU1

function NGX_DU1_LOWER_show ()
local txt
local pos = ipc.readLvar('ngx_switch_336_a')
    if pos == 0 then txt ="ENG"
    elseif pos == 50 then txt = "NORM"
    elseif pos == 100 then txt = "ND"
    end
    DspShow ("DUlw", txt)
end

function NGX_DU1_LOWER_ENG ()
    ipc.control(PMDGBaseVar+336, 0)
    ipc.sleep(50)
    NGX_DU1_LOWER_show ()
end

function NGX_DU1_LOWER_NORM ()
    ipc.control(PMDGBaseVar+336, 1)
    ipc.sleep(50)
    NGX_DU1_LOWER_show ()
end

function NGX_DU1_LOWER_ND ()
    ipc.control(PMDGBaseVar+336, 2)
    ipc.sleep(50)
    NGX_DU1_LOWER_show ()
end

function NGX_DU1_LOWER_inc ()
    ipc.control(PMDGBaseVar+336, PMDG_ClkR)
    NGX_DU1_LOWER_show ()
end

function NGX_DU1_LOWER_dec ()
    ipc.control(PMDGBaseVar+336, PMDG_ClkL)
    NGX_DU1_LOWER_show ()
end

----------------------

-- ## Displays - Modes DU2 ###############

--- DU2

function NGX_DU2_MAIN_show ()
local txt
local pos = ipc.readLvar('ngx_switch_440_a')
    if pos == 0 then txt ="oPFD"
    elseif pos == 10 then txt = "NORM"
    elseif pos == 20 then txt = "iENG"
    elseif pos == 30 then txt = "iPFD"
    elseif pos == 40 then txt = "iMFD"
    end
    DspShow ("DUmn", txt)
end

function NGX_DU2_MAIN_oPFD ()
    ipc.control(PMDGBaseVar+440, 0)
    ipc.sleep(50)
    NGX_DU2_MAIN_show ()
end

function NGX_DU2_MAIN_norm ()
    ipc.control(PMDGBaseVar+440, 1)
    ipc.sleep(50)
    NGX_DU2_MAIN_show ()
end

function NGX_DU2_MAIN_iENG ()
    ipc.control(PMDGBaseVar+440, 2)
    ipc.sleep(50)
    NGX_DU2_MAIN_show ()
end

function NGX_DU2_MAIN_iPFD ()
    ipc.control(PMDGBaseVar+440, 3)
    ipc.sleep(50)
    NGX_DU2_MAIN_show ()
end

function NGX_DU2_MAIN_iMFD ()
    ipc.control(PMDGBaseVar+440, 4)
    ipc.sleep(50)
    NGX_DU2_MAIN_show ()
end

function NGX_DU2_MAIN_inc ()
    ipc.control(PMDGBaseVar+440, PMDG_ClkR)
    NGX_DU2_MAIN_show ()
end

function NGX_DU2_MAIN_dec ()
    ipc.control(PMDGBaseVar+440, PMDG_ClkL)
    NGX_DU2_MAIN_show ()
end

------

function NGX_DU2_LOWER_show ()
local txt
local pos = ipc.readLvar('ngx_switch_441_a')
    if pos == 0 then txt ="ENG"
    elseif pos == 50 then txt = "NORM"
    elseif pos == 100 then txt = "ND"
    end
    DspShow ("DUlw", txt)
end

function NGX_DU2_LOWER_inc ()
    ipc.control(PMDGBaseVar+441, PMDG_ClkR)
    NGX_DU2_LOWER_show ()
end

function NGX_DU2_LOWER_dec ()
    ipc.control(PMDGBaseVar+441, PMDG_ClkL)
    NGX_DU2_LOWER_show ()
end

function NGX_DU2_LOWER_ENG ()
    ipc.control(PMDGBaseVar+441, 0)
    NGX_DU2_LOWER_show ()
end

function NGX_DU2_LOWER_NORM ()
    ipc.control(PMDGBaseVar+441, 1)
    NGX_DU2_LOWER_show ()
end

function NGX_DU2_LOWER_ND ()
    ipc.control(PMDGBaseVar+441, 2)
    NGX_DU2_LOWER_show ()
end

function NGX_DU_LOWER_sys ()
    ipc.control(PMDGBaseVar+462, PMDG_ClkL)
    DspShow ("DUlw", "SYS")
end

function NGX_DU_LOWER_eng ()
    ipc.control(PMDGBaseVar+463, PMDG_ClkL)
    DspShow ("DUlw", "ENG")
end

-- ## Other ###############

function NGX_YAW_DAMPER_on ()
    if ipc.readLvar('ngx_switch_63_a') == 0 then
        ipc.control(PMDGBaseVar+63, PMDG_ClkL)
        DspShow ("YawD", " on ")
    end
end

function NGX_YAW_DAMPER_off ()
    if ipc.readLvar('ngx_switch_63_a') == 100 then
        ipc.control(PMDGBaseVar+63, PMDG_ClkL)
        DspShow ("YawD", " off")
    end
end

function NGX_YAW_DAMPER_toggle ()
	if _tl("ngx_switch_63_a", 0) then
         NGX_YAW_DAMPER_on ()
	else
         NGX_YAW_DAMPER_off ()
	end
end

--------

function NGX_SPOILER_off ()
    if ipc.readLvar('ngx_switch_679_a') ~= 0 then
        ipc.control(PMDGBaseVar+6792, PMDG_ClkL)
        ipc.control(86637, -16384)
        DspShow ("SPLR", "off")
    end
end

function NGX_SPOILER_arm ()
    if ipc.readLvar('ngx_switch_679_a') ~= 100 then
        ipc.control(PMDGBaseVar+6792, PMDG_ClkR)
        ipc.control(86637, -13100)
        DspShow ("SPLR", "arm")
    end
end

function NGX_SPOILER_50 ()
    if ipc.readLvar('ngx_switch_679_a') ~= 200 then
        ipc.control(86637, -2200)
        DspShow ("SPLR", "50%")
    end
end

function NGX_SPOILER_detent ()
    if ipc.readLvar('ngx_switch_679_a') ~= 300 then
        ipc.control(86637, 7100)
        DspShow ("SPLR", "FDet")
    end
end

function NGX_SPOILER_100 ()
    if ipc.readLvar('ngx_switch_679_a') ~= 400 then
        ipc.control(86637, 16384)
        DspShow ("SPLR", "up")
    end
end

----

function NGX_GEAR_show ()
local txt
local pos = ipc.readLvar('ngx_switch_455_a')
    if pos < 30 then txt ="up  "
    elseif pos == 30 then txt = "off "
    elseif pos > 30 then txt = "down"
    end
    DspShow ("GEAR", txt)
end

function NGX_GEAR_up ()
    ipc.control(PMDGBaseVar+455, PMDG_ClkR)
--    if _MCP1()  then
--        DspShow ("GEAR", ">>UP")
--        ipc.sleep(300)
--        NGX_GEAR_show ()
--    end
end

function NGX_GEAR_down ()
    ipc.control(PMDGBaseVar+455, PMDG_ClkL)
--    if _MCP1()  then
--        DspShow ("GEAR", ">>DN")
--        ipc.sleep(300)
--        NGX_GEAR_show ()
--    end
end

function NGX_GEAR_off ()
local pos = ipc.readLvar('ngx_switch_455_a')
    if pos > 30 then
        ipc.control(PMDGBaseVar+455, PMDG_ClkR)
    elseif pos < 30 then
        ipc.control(PMDGBaseVar+455, PMDG_ClkL)
    end
--    if _MCP1()  then
--        DspShow ("GEAR", ">off")
--        ipc.sleep(300)
--        NGX_GEAR_show ()
--    end
end

-- ## Headup Guidance System (HGS) -------------

function NGX_HGS_down ()
     if ipc.readLvar('ngx_switch_979_a') == 100 then
         ipc.control(PMDGBaseVar+979, PMDG_ClkR)
     end
     DspShow("HUD", "down")
end

function NGX_HGS_up ()
     if ipc.readLvar('ngx_switch_979_a') == 0 then
     ipc.control(PMDGBaseVar+979, PMDG_ClkL)
     end
     DspShow("HUD", "up")
end

function NGX_HGS_toggle ()
	if _tl("ngx_switch_979_a", 100) then
       NGX_HGS_down ()
	else
       NGX_HGS_up ()
	end
end

function NGX_HGS_mode ()
    ipc.control(PMDGBaseVar+770, PMDG_ClkL)
end

-- ## Signs, Chimes & CVR  -------------

function NGX_SIGNS_CHIME_on ()
    if ipc.readLvar('ngx_switch_103_a') == 0 then
        ipc.control(PMDGBaseVar+103, PMDG_ClkL)
		DspShow ("CHME", "on")
    end
end

function NGX_SIGNS_CHIME_off ()
    if ipc.readLvar('ngx_switch_103_a') == 100 then
        ipc.control(PMDGBaseVar+103, PMDG_ClkR)
		DspShow ("CHME", "off")
    end
end

function NGX_SIGNS_CHIME_toggle ()
	if _tl("ngx_switch_103_a", 0) then
        NGX_SIGNS_CHIME_on ()
	else
        NGX_SIGNS_CHIME_off ()
	end
end

---

function NGX_SIGNS_SEAT_on ()
    if ipc.readLvar('ngx_switch_104_a') ~= 100 then
        -- do not delete - next line required to work
        ipc.writeLvar('ngx_switch_104_a', 100)
        ipc.control(PMDGBaseVar+104, PMDG_ClkL)
        ipc.control(PMDGBaseVar+104, PMDG_ClkL)
		DspShow ("SEAT", "on")
    end
end

function NGX_SIGNS_SEAT_auto ()
local pos = ipc.readLvar('ngx_switch_104_a')
    if pos > 50 then
        -- do not delete - next line required to work
        ipc.writeLvar('ngx_switch_104_a', 50)
        ipc.control(PMDGBaseVar+104, PMDG_ClkR)
        DspShow ("SEAT", "auto")
    elseif pos < 50 then
        -- do not delete - next line required to work
        ipc.writeLvar('ngx_switch_104_a', 50)
        ipc.control(PMDGBaseVar+104, PMDG_ClkL)
        DspShow ("SEAT", "auto")
    end
end

function NGX_SIGNS_SEAT_off ()
    if ipc.readLvar('ngx_switch_104_a') ~= 0 then
        -- do not delete - next line required to work
        ipc.writeLvar('ngx_switch_104_a', 0)
        ipc.control(PMDGBaseVar+104, PMDG_ClkR)
        ipc.control(PMDGBaseVar+104, PMDG_ClkR)
		DspShow ("SEAT", "off")
    end
end

---

function NGX_CVR_test ()
    if ipc.readLvar('ngx_switch_178_a') ~= 100 then
        -- do not delete - next line required to work
        ipc.writeLvar('ngx_switch_178_a', 100)
        ipc.control(PMDGBaseVar+178, PMDG_ClkL)
		DspShow ("CVR", "test")
        ipc.sleep(5000)
        NGX_CVR_test_off()
    end
end

---

function NGX_CVR_test_off ()
    if ipc.readLvar('ngx_switch_178_a') ~= 0 then
        -- do not delete - next line required to work
        ipc.writeLvar('ngx_switch_178_a', 0)
        ipc.control(PMDGBaseVar+178, PMDG_ClkL)
		DspShow ("CVR", "off")
    end
end

---
function NGX_CVR_erase ()
    if ipc.readLvar('ngx_switch_180_a') ~= 100 then
        -- do not delete - next line required to work
        ipc.writeLvar('ngx_switch_180_a', 100)
        ipc.control(PMDGBaseVar+180, PMDG_ClkL)
		DspShow ("CVR", "eras")
        ipc.sleep(100)
        NGX_CVR_erase_off()
    end
end

---

function NGX_CVR_erase_off ()
    if ipc.readLvar('ngx_switch_180_a') ~= 0 then
        -- do not delete - next line required to work
        ipc.writeLvar('ngx_switch_180_a', 0)
        ipc.control(PMDGBaseVar+180, PMDG_ClkL)
		--DspShow ("CVR", "off")
    end
end

---

function NGX_ALT_Horn_Cutout ()
    ipc.control(PMDGBaseVar+183, PMDG_ClkL)
    ipc.sleep(250)
end

-- standby compass light located below centre of OVH

function NGX_Compass_off ()
    ipc.control(PMDGBaseVar+982, 1)
    DspShow ("CMPS", "off", "COMPASS", "off")
end

function NGX_Compass_dim ()
    ipc.control(PMDGBaseVar+982, 0)
    DspShow ("CMPS", "dim", "COMPASS", "dim")
end

function NGX_Compass_brt ()
    ipc.control(PMDGBaseVar+982, 2)
    DspShow ("CMPS", "brt", "COMPASS", "brt")
end

 -- ## WX RADAR panel -------------------

function WXR_L_TFR ()
	if ipc.readLvar("ngx_switch_790_a") == 0 then
        DspShow ("WXR", "TFR")
    	ipc.control(PMDGBaseVar+790, PMDG_ClkL)
    else
        ipc.control(PMDGBaseVar+790, PMDG_ClkL)
    end
end

function WXR_L_WX ()
	if ipc.readLvar("ngx_switch_791_a") == 0 then
    	ipc.control(PMDGBaseVar+791, PMDG_ClkL)
        DspShow ("WXR", "WX")
    end
end

function WXR_L_WX_T ()
	if ipc.readLvar("ngx_switch_916_a") == 0 then
    	ipc.control(PMDGBaseVar+916, PMDG_ClkL)
        DspShow ("WXR", "WX+T")
	end
end

function WXR_L_MAP ()
	if ipc.readLvar("ngx_switch_792_a") == 0 then
	   ipc.control(PMDGBaseVar+792, PMDG_ClkL)
	   DspShow ("WXR", "MAP")
	end
end

function WXR_L_GC ()
	if ipc.readLvar("ngx_switch_793_a") == 0 then
	   ipc.control(PMDGBaseVar+793, PMDG_ClkL)
	   DspShow ("WXR", "GC")
       ipc.sleep(500)
       ipc.control(PMDGBaseVar+793, 0)
	end
end

function WXR_AUTO ()
	if ipc.readLvar("ngx_switch_917_a") == 0 then
    	ipc.control(PMDGBaseVar+917, PMDG_ClkL)
	   DspShow ("WXR", "AUTO")
    else
        ipc.control(PMDGBaseVar+917, PMDG_ClkL)
    end
end

--function WXR_L_R ()
--	if ipc.readLvar("switch_584_a") == 0 then
--	ipc.control(PMDGBaseVar+584,1)
--	DspShow ("WXR", "L-R")
--	end
--end

function WXR_TEST ()
	if ipc.readLvar("ngx_switch_585_a") == 0 then
	   ipc.control(PMDGBaseVar+918,PMDG_ClkL)
	   DspShow ("WXR", "test")
       ipc.sleep(500)
       ipc.control(PMDGBaseVar+918, 0)
	end
end

function WXR_R_TFR ()
	if ipc.readLvar("ngx_switch_919_a") == 0 then
    	ipc.control(PMDGBaseVar+919, PMDG_ClkL)
	   DspShow ("WXR", "tfr", "WXRr", "tfr")
    else
        ipc.control(PMDGBaseVar+919, PMDG_ClkL)
    end
end

function WXR_R_WX ()
	if ipc.readLvar("ngx_switch_796_a") == 0 then
    	ipc.control(PMDGBaseVar+796, PMDG_ClkL)
	   DspShow ("WXR", "wx", "WXRr", "wx")
    end
end

function WXR_R_WX_T ()
	if ipc.readLvar("ngx_switch_920_a") == 0 then
    	ipc.control(PMDGBaseVar+920, PMDG_ClkL)
	   DspShow ("WXR", "wx-t", "WXRr", "wx-t")
	end
end

function WXR_R_MAP ()
	if ipc.readLvar("ngx_switch_797_a") == 0 then
	   ipc.control(PMDGBaseVar+797, PMDG_ClkL)
	   DspShow ("WXR", "map", "WXRr", "map")
	end
end

function WXR_R_GC ()
	if ipc.readLvar("ngx_switch_921_a") == 0 then
	   ipc.control(PMDGBaseVar+921, PMDG_ClkL)
	   DspShow ("WXR", "gc", "WXR R", "gc")
       ipc.sleep(500)
       ipc.control(PMDGBaseVar+921, 0)
	end
end

function WXR_L_TILT_CONTROL_inc ()
    WXRLtilt = ipc.readLvar("ngx_switch_794_a")
    if WXRLtilt == 21 then WXRLtilt = 21 end
    ipc.control(PMDGBaseVar+794, WXRLtilt+1)
	DspShow ("TILT", "inc", "WXR TILT", "inc" .. WXRLtilt )
end

function WXR_L_TILT_CONTROL_dec ()
	WXRLtilt = ipc.readLvar("ngx_switch_794_a")
    if WXRLtilt == 0 then WXRLtilt = 0 end
    ipc.control(PMDGBaseVar+794, WXRLtilt-1)
    DspShow ("TILT", "dec", "WXR TILT", "dec")
end

---

function WXR_L_GAIN_CONTROL_inc ()
	WXRLGAIN = ipc.readLvar("ngx_switch_923_a")
    if WXRLGAIN == 21 then WXRLGAIN = 21 end
    ipc.control(PMDGBaseVar+923, WXRLGAIN+1)
   	DspShow ("GAIN", "inc", "WXR GAIN", "inc")
end

function WXR_L_GAIN_CONTROL_dec ()
	WXRLGAIN = ipc.readLvar("ngx_switch_923_a")
    if WXRLGAIN == 0 then WXRLGAIN = 0 end
    ipc.control(PMDGBaseVar+923, WXRLGAIN-1)
    DspShow ("GAIN", "dec", "WXR GAIN", "dec")
end

---
function WXR_R_TILT_CONTROL_inc ()
	WXRLtilt = ipc.readLvar("ngx_switch_795_a")
    if WXRLtilt == 21 then WXRLtilt = 21 end
    ipc.control(PMDGBaseVar+795, WXRLtilt+1)
    DspShow ("TILT", "inc", "WXR TILT", "inc")
end

function WXR_R_TILT_CONTROL_dec ()
	WXRLtilt = ipc.readLvar("ngx_switch_795_a")
    if WXRLtilt == 0 then WXRLtilt = 0 end
    ipc.control(PMDGBaseVar+795, WXRLtilt-1)
    DspShow ("TILT", "dec", "WXR TILT", "dec")
end

----

function WXR_R_GAIN_CONTROL_inc ()
	WXRLGAIN = ipc.readLvar("ngx_switch_922_a")
    if WXRLGAIN == 21 then WXRLGAIN = 21 end
    ipc.control(PMDGBaseVar+922, WXRLGAIN+1)
    DspShow ("GAIN", "inc", "WXR GAIN", "inc")
end

function WXR_R_GAIN_CONTROL_dec ()
	WXRLGAIN = ipc.readLvar("ngx_switch_922_a")
    if WXRLGAIN == 0 then WXRLGAIN = 0 end
    ipc.control(PMDGBaseVar+922, WXRLGAIN-1)
    DspShow ("GAIN", "dec", "WXR GAIN", "dec")
end

-- ## Windscreen Wiper ###############

function NGX_Wiper_R_show ()
local txt
local pos = ipc.readLvar('ngx_switch_109_a')
    if pos == 0 then txt ="park"
    elseif pos == 10 then txt = "int "
    elseif pos == 20 then txt = "low"
    elseif pos == 30 then txt = "high"
    end
    DspShow ("WipR", txt)
end

function NGX_Wiper_R_calc (sel)
if sel == nil then return end
local pos = ipc.readLvar("L:ngx_switch_109_a")/10
    if pos > sel then
        for i = 1, (pos - sel), 1 do
            NGX_Wiper_R_dec ()
        end
    elseif pos < sel then
        for i = 1, (sel - pos), 1 do
            NGX_Wiper_R_inc ()
        end
    end
end

function NGX_Wiper_R_inc ()
local pos = ipc.readLvar('ngx_switch_109_a')
    if true then --pos < 30 then
        ipc.control(PMDGBaseVar+109, PMDG_inc)
        NGX_Wiper_R_show ()
    end
end

function NGX_Wiper_R_dec ()
local pos = ipc.readLvar('ngx_switch_109_a')
    if true then --pos > 0 then
        ipc.control(PMDGBaseVar+109, PMDG_dec)
        NGX_Wiper_R_show ()
    end
end

function NGX_Wiper_R_cycle ()
local pos = ipc.readLvar('ngx_switch_109_a')
    if pos < 30 then
        NGX_Wiper_R_inc()
    else
        NGX_Wiper_R_park()
    end
end

function NGX_Wiper_R_park ()
    if ipc.readLvar('ngx_switch_109_a') ~= 0 then
        NGX_Wiper_R_calc (0)
        NGX_Wiper_R_show ()
    end
end

function NGX_Wiper_R_int ()
    if ipc.readLvar('ngx_switch_109_a') ~= 10 then
        NGX_Wiper_R_calc (1)
        NGX_Wiper_R_show ()
    end
end

function NGX_Wiper_R_low ()
    if ipc.readLvar('ngx_switch_109_a') ~= 20 then
        NGX_Wiper_R_calc (2)
        NGX_Wiper_R_show ()
    end
end

function NGX_Wiper_R_high ()
    if ipc.readLvar('ngx_switch_109_a') ~= 30 then
        NGX_Wiper_R_calc (3)
        NGX_Wiper_R_show ()
    end
end

----------------------------------

function NGX_Wiper_L_show ()
local txt
local pos = ipc.readLvar('ngx_switch_36_a')
    if pos == 0 then txt ="park"
    elseif pos == 10 then txt = "int "
    elseif pos == 20 then txt = "low"
    elseif pos == 30 then txt = "high"
    end
    DspShow ("WipR", txt)
end

function NGX_Wiper_L_calc (sel)
if sel == nil then return end
local pos = ipc.readLvar("L:ngx_switch_36_a")/10
    if pos > sel then
        for i = 1, (pos - sel), 1 do
            NGX_Wiper_L_dec ()
        end
    elseif pos < sel then
        for i = 1, (sel - pos), 1 do
            NGX_Wiper_L_inc ()
        end
    end
end

function NGX_Wiper_L_inc ()
local pos = ipc.readLvar('ngx_switch_36_a')
    if true then --pos < 30 then
        ipc.control(PMDGBaseVar+36, PMDG_inc)
        NGX_Wiper_L_show ()
    end
end

function NGX_Wiper_L_dec ()
local pos = ipc.readLvar('ngx_switch_36_a')
    if true then --pos > 0 then
        ipc.control(PMDGBaseVar+36, PMDG_dec)
        NGX_Wiper_L_show ()
    end
end

function NGX_Wiper_L_cycle ()
local pos = ipc.readLvar('ngx_switch_36_a')
    if pos < 30 then
        NGX_Wiper_L_inc()
    else
        NGX_Wiper_L_park()
    end
end

function NGX_Wiper_L_park ()
    if ipc.readLvar('ngx_switch_36_a') ~= 0 then
        NGX_Wiper_L_calc (0)
        NGX_Wiper_L_show ()
    end
end

function NGX_Wiper_L_int ()
    if ipc.readLvar('ngx_switch_36_a') ~= 10 then
        NGX_Wiper_L_calc (1)
        NGX_Wiper_L_show ()
    end
end

function NGX_Wiper_L_low ()
    if ipc.readLvar('ngx_switch_36_a') ~= 20 then
        NGX_Wiper_L_calc (2)
        NGX_Wiper_L_show ()
    end
end

function NGX_Wiper_L_high ()
    if ipc.readLvar('ngx_switch_36_a') ~= 30 then
        NGX_Wiper_L_calc (3)
        NGX_Wiper_L_show ()
    end
end

------------------------------

function NGX_Wiper_both_inc ()
    NGX_Wiper_L_inc ()
    _sleep(150, 250)
    NGX_Wiper_R_inc ()
end

function NGX_Wiper_both_dec ()
    NGX_Wiper_L_dec ()
    _sleep(150, 250)
    NGX_Wiper_R_dec ()
end

-- ## Trimmings ###############

-- $$ Elevator Trim

function NGX_ELEV_TRIM_up ()
    ipc.control(65615)
    ipc.sleep(150)
    --NGX_ELEV_TRIM_show ()
end

function NGX_ELEV_TRIM_down ()
    ipc.control(65607)
    ipc.sleep(150)
    --NGX_ELEV_TRIM_show ()
end

function NGX_ELEV_TRIM_upfast ()
    ipc.control(65615)
    ipc.sleep(50)
    ipc.control(65615)
    ipc.sleep(50)
    ipc.control(65615)
    ipc.sleep(50)
    ipc.control(65615)
    ipc.sleep(50)
    ipc.control(65615)
    ipc.sleep(50)
    ipc.control(65615)
    ipc.sleep(50)
    ipc.control(65615)
    ipc.sleep(50)
    ipc.control(65615)
    ipc.sleep(150)
    --NGX_ELEV_TRIM_show ()
end

function NGX_ELEV_TRIM_downfast ()
    ipc.control(65607)
    ipc.sleep(50)
    ipc.control(65607)
    ipc.sleep(50)
    ipc.control(65607)
    ipc.sleep(50)
    ipc.control(65607)
    ipc.sleep(50)
    ipc.control(65607)
    ipc.sleep(50)
    ipc.control(65607)
    ipc.sleep(50)
    ipc.control(65607)
    ipc.sleep(50)
    ipc.control(65607)
    ipc.sleep(150)
    --NGX_ELEV_TRIM_show ()
end

function NGX_ELEV_TRIM_show ()
    ETrimDsp = round2((ipc.readLvar('ElevTrimTT')), 2)
    if _MCP1() then
        DspShow("ETrm", ETrimDsp)
    elseif _MCP2() then
        DspMed1("ElevTrim")
        DspMed2(ETrimDsp)
    elseif _MCP2a() then
        DspMed1("ElevTrm")
        DspMed2(ETrimDsp)
    end
end

-- $$ Aileron Trim

function NGX_AIL_TRIM_show ()
    AilTrimSwitch = ipc.readLvar("ngx_switch_810_a")
    ATrimDsp = round2((ipc.readLvar('AileronTrimTT')), 2)
    if _MCP1() then
        DspShow("ATrm", ATrimDsp)
    elseif _MCP2() then
        if AilTrimSwitch == 0 then
            ATrimDir = "L"
        elseif AilTrimSwitch == 100 then
            ATrimDir = "R"
        end
        DspMed1("AilTrim" .. ATrimDir)
        DspMed2(ATrimDsp)
    elseif _MCP2a() then
        if AilTrimSwitch == 0 then
            ATrimDir = "L"
        elseif AilTrimSwitch == 100 then
            ATrimDir = "R"
        end
        DspMed1("AilTrm" .. ATrimDir)
        DspMed2(ATrimDsp)
    end
end

function NGX_AIL_TRIM_leftWingDown ()
    ipc.control(70442, PMDG_ClkL)
    NGX_AIL_TRIM_show ()
end

function NGX_AIL_TRIM_rightWingDown ()
    ipc.control(70442, PMDG_ClkR)
    NGX_AIL_TRIM_show ()
end

function NGX_AIL_TRIM_stop ()
    AilTrimSwitch = ipc.readLvar("ngx_switch_810_a")
    if AilTrimSwitch == 0 then
        ipc.control(70442, PMDG_ClkR)
    elseif AilTrimSwitch == 100 then
        ipc.control(70442, PMDG_ClkL)
    end
    NGX_AIL_TRIM_show ()
end

-- $$ Rudder Trim

function NGX_RUD_TRIM_show ()
    RudTrimSwitch = ipc.readLvar("ngx_switch_811_a")
    RTrimDsp = round2((ipc.readLvar('RudderTrimTT')), 2)
    if _MCP1() then
        DspShow("RTrm", RTrimDsp)
    elseif _MCP2() then
        if RudTrimSwitch == 0 then
            RTrimDir = "<<"
        elseif RudTrimSwitch == 100 then
            RTrimDir = ">>"
        elseif RudTrimSwitch == 50 then
            RTrimDir = ""
        end
        DspMed1("RuddTrim")
        DspMed2(RTrimDsp .. " ".. RTrimDir)
    elseif _MCP2a() then
        if RudTrimSwitch == 0 then
            RTrimDir = "<"
        elseif RudTrimSwitch == 100 then
            RTrimDir = ">"
        elseif RudTrimSwitch == 50 then
            RTrimDir = ""
        end
        DspMed1("RudTrm" .. RTrimDir)
        DspMed2(RTrimDsp)
    end
end

function NGX_RUD_TRIM_left ()
    ipc.control(70443, PMDG_ClkL)
    NGX_RUD_TRIM_show ()
end

function NGX_RUD_TRIM_right ()
    ipc.control(70443, PMDG_ClkR)
    NGX_RUD_TRIM_show ()
end

function NGX_RUD_TRIM_stop ()
    RudTrimSwitch = ipc.readLvar("ngx_switch_811_a")
    if RudTrimSwitch == 0 then
        ipc.control(70443, PMDG_ClkR)
    elseif RudTrimSwitch == 100 then
        ipc.control(70443, PMDG_ClkL)
    end
    NGX_RUD_TRIM_show ()
end

-- ## Transponder ###############

function NGX_TCAS_test ()
    ipc.control(70433, PMDG_ClkL)
    DspShow ("TCAS", "TEST")
end

function NGX_XPND_MODE_inc ()
    ipc.control(70432, PMDG_inc)
    NGX_XPND_MODE_show ()
end

function NGX_XPND_MODE_dec ()
    ipc.control(70432, PMDG_dec)
    NGX_XPND_MODE_show ()
end

function NGX_XPND_MODE_cycle ()
    if ipc.readLvar('ngx_switch_800_a') < 40 then
        NGX_XPND_MODE_inc ()
    else
        ipc.control(70432, PMDG_dec)
        ipc.control(70432, PMDG_dec)
        ipc.control(70432, PMDG_dec)
        NGX_XPND_MODE_dec ()
    end
end

function NGX_TOGGLE_STBY_TARA()

  XPNDvar = ipc.readLvar('ngx_switch_800_a')/10
    
  if XPNDvar > 0 then
    NGX_XPND_MODE_dec ()
    NGX_XPND_MODE_dec ()
    NGX_XPND_MODE_dec ()
    NGX_XPND_MODE_dec ()
  else
    NGX_XPND_MODE_inc ()
    NGX_XPND_MODE_inc ()
    NGX_XPND_MODE_inc ()
    NGX_XPND_MODE_inc ()

  end
end

function NGX_XPND_MODE_tara ()
    i = 0
    XPNDset = 4
    XPNDvar = ipc.readLvar('ngx_switch_800_a')/10
    if XPNDvar < XPNDset then
        XPNDval = XPNDset-XPNDvar
        for i = 1, XPNDval, 1 do
            NGX_XPND_MODE_inc ()
        end
    elseif XPNDvar > XPNDset then
        XPNDval = XPNDvar-XPNDset
        for i = 1, XPNDval, 1 do
            NGX_XPND_MODE_dec ()
        end
    end
    NGX_XPND_MODE_show ()
end

function NGX_XPND_MODE_ta ()
    i = 0
    XPNDset = 3
    XPNDvar = ipc.readLvar('ngx_switch_800_a')/10
    if XPNDvar < XPNDset then
        XPNDval = XPNDset-XPNDvar
        for i = 1, XPNDval, 1 do
            NGX_XPND_MODE_inc ()
        end
    elseif XPNDvar > XPNDset then
        XPNDval = XPNDvar-XPNDset

        for i = 1, XPNDval, 1 do
            NGX_XPND_MODE_dec ()
        end
    end
    NGX_XPND_MODE_show ()
end

function NGX_XPND_MODE_xpdr ()
    i = 0
    XPNDset = 2
    XPNDvar = ipc.readLvar('ngx_switch_800_a')/10
    if XPNDvar < XPNDset then
        XPNDval = XPNDset-XPNDvar
        for i = 1, XPNDval, 1 do
            NGX_XPND_MODE_inc ()
        end
    elseif XPNDvar > XPNDset then
        XPNDval = XPNDvar-XPNDset
        for i = 1, XPNDval, 1 do
            NGX_XPND_MODE_dec ()
        end
    end
    NGX_XPND_MODE_show ()
end

function NGX_XPND_MODE_rptg ()
    i = 0
    XPNDset = 1
    XPNDvar = ipc.readLvar('ngx_switch_800_a')/10
    if XPNDvar < XPNDset then
        XPNDval = XPNDset-XPNDvar
        for i = 1, XPNDval, 1 do
            NGX_XPND_MODE_inc ()
        end
    elseif XPNDvar > XPNDset then
        XPNDval = XPNDvar-XPNDset
        for i = 1, XPNDval, 1 do
            NGX_XPND_MODE_dec ()
        end
    end
    NGX_XPND_MODE_show ()
end

function NGX_XPND_MODE_stby ()
    i = 0
    XPNDset = 0
    XPNDvar = ipc.readLvar('ngx_switch_800_a')/10
    if XPNDvar < XPNDset then
        XPNDval = XPNDset-XPNDvar
        for i = 1, XPNDval, 1 do
            NGX_XPND_MODE_inc ()
        end
    elseif XPNDvar > XPNDset then
        XPNDval = XPNDvar-XPNDset
        for i = 1, XPNDval, 1 do
            NGX_XPND_MODE_dec ()
        end
    end
    NGX_XPND_MODE_show ()
end

function NGX_XPND_MODE_show ()
    if ipc.readLvar('ngx_switch_800_a') == 0 then
        if _MCP1() then
            DspShow ("XPND", "STBY")
        else
            DspRadioShort("STBY")
        end
    elseif ipc.readLvar('ngx_switch_800_a') == 10 then
        if _MCP1() then
            DspShow ("XPND", "RPTG")
        else
            DspRadioShort("RPTG")
        end
    elseif ipc.readLvar('ngx_switch_800_a') == 20 then
        if _MCP1() then
            DspShow ("XPND", "XPND")
        else
            DspRadioShort("XPND")
        end
    elseif ipc.readLvar('ngx_switch_800_a') == 30 then
        if _MCP1() then
            DspShow ("XPND", "TA")
        else
            DspRadioShort("TA")
        end
    elseif ipc.readLvar('ngx_switch_800_a') == 40 then
        if _MCP1() then
            DspShow ("XPND", "TARA")
        else
            DspRadioShort("TARA")
        end
    end
end

-- ## Main Panel Misc ##################################

-- $$ LIGHTS Switch
-- light switch located on capt's panel above rh DU

function NGX_LIGHTS_dim ()
     if ipc.readLvar('ngx_switch_346_a') ~= 100 then
        ipc.control(PMDGBaseVar+346, PMDG_ClkL)
        ipc.control(PMDGBaseVar+346, PMDG_ClkL)
		DspShow("LGHT", "dim ")
    end
end

function NGX_LIGHTS_bright ()
local pos = ipc.readLvar('ngx_switch_346_a')
    if pos > 50 then
        ipc.control(PMDGBaseVar+346, PMDG_ClkR)
        DspShow("LGHT", "brt ")
    elseif pos < 50 then
        ipc.control(PMDGBaseVar+346, PMDG_ClkL)
        DspShow("LGHT", "brt ")
    end
end

function NGX_LIGHTS_test ()
     if ipc.readLvar('ngx_switch_346_a') ~= 0 then
        ipc.control(PMDGBaseVar+346, PMDG_ClkR)
        ipc.control(PMDGBaseVar+346, PMDG_ClkR)
		DspShow("LGHT", "test")
     end
end

function NGX_LIGHTS_TEST_toggle ()
     if ipc.readLvar('ngx_switch_346_a') == 0 then
        NGX_LIGHTS_bright ()
     else
        NGX_LIGHTS_test ()
     end
end

function NGX_LIGHTS_cycle ()
local pos = ipc.readLvar('ngx_switch_346_a')
    if pos == 100 then
        NGX_LIGHTS_bright ()
     elseif pos == 50 then
        NGX_LIGHTS_test ()
     else
        NGX_LIGHTS_dim ()
    end
end

function NGX_LIGHTS_toggle ()
     if ipc.readLvar('ngx_switch_346_a') ~= 50 then
        NGX_LIGHTS_bright ()
     else
        NGX_LIGHTS_dim ()
    end
end

-- $$ Auto Brake
-- 460

function NGX_AUTOBRAKE_show ()
local txt
local pos = ipc.readLvar('ngx_switch_460_a')
    if pos == 0 then txt ="rto"
    elseif pos == 10 then txt = "off"
    elseif pos == 20 then txt = "1"
    elseif pos == 30 then txt = "2"
    elseif pos == 40 then txt = "3"
    elseif pos == 50 then txt = "max"
    end
    DspShow ("ABRK", txt)
end

function NGX_AUTOBRAKE_calc (sel)
if sel == nil then return end
local pos = ipc.readLvar("L:ngx_switch_460_a")/10
    if pos > sel then
        for i = 1, (pos - sel), 1 do
            NGX_AUTOBRAKE_dec ()
        end
    elseif pos < sel then
        for i = 1, (sel - pos), 1 do
            NGX_AUTOBRAKE_inc ()
        end
    end
end

function NGX_AUTOBRAKE_inc ()
    if ipc.readLvar("L:ngx_switch_460_a") < 50 then
        ipc.control(PMDGBaseVar+460, PMDG_inc)
        NGX_AUTOBRAKE_show ()
    end
end

function NGX_AUTOBRAKE_dec ()
    if ipc.readLvar("L:ngx_switch_460_a") > 0 then
        ipc.control(PMDGBaseVar+460, PMDG_dec)
        NGX_AUTOBRAKE_show ()
    end
end

function NGX_AUTOBRAKE_cycle ()
    if ipc.readLvar("L:ngx_switch_460_a") < 50 then
        ipc.control(PMDGBaseVar+460, PMDG_inc)
    else
        NGX_AUTOBRAKE_RTO ()
    end
end

function NGX_AUTOBRAKE_RTO ()
    if ipc.readLvar("L:ngx_switch_460_a") ~= 0 then
        NGX_AUTOBRAKE_calc (0)
        NGX_AUTOBRAKE_show ()
    end
end

function NGX_AUTOBRAKE_OFF ()
    if ipc.readLvar("L:ngx_switch_460_a") ~= 10 then
        NGX_AUTOBRAKE_calc (1)
        NGX_AUTOBRAKE_show ()
    end
end

function NGX_AUTOBRAKE_1 ()
    if ipc.readLvar("L:ngx_switch_460_a") ~= 20 then
        NGX_AUTOBRAKE_calc (2)
        NGX_AUTOBRAKE_show ()
    end
end

function NGX_AUTOBRAKE_2 ()
    if ipc.readLvar("L:ngx_switch_460_a") ~= 30 then
        NGX_AUTOBRAKE_calc (3)
        NGX_AUTOBRAKE_show ()
    end
end

function NGX_AUTOBRAKE_3 ()
    if ipc.readLvar("L:ngx_switch_460_a") ~= 40 then
        NGX_AUTOBRAKE_calc (4)
        NGX_AUTOBRAKE_show ()
    end
end

function NGX_AUTOBRAKE_MAX ()
    if ipc.readLvar("L:ngx_switch_460_a") ~= 50 then
        NGX_AUTOBRAKE_calc (5)
        NGX_AUTOBRAKE_show ()
    end
end

-- $$ MFD Selector Buttons
-- 462, 463, 4621

function NGX_MFD_SELECTOR_SYS ()
    ipc.control(PMDGBaseVar+462, PMDG_ClkL)
end

function NGX_MFD_SELECTOR_ENG ()
    ipc.control(PMDGBaseVar+463, PMDG_ClkL)
end

function NGX_MFD_SELECTOR_CR ()
    ipc.control(PMDGBaseVar+4621, PMDG_ClkL)
end

-- $$ Speed Reference Selector
-- located above centre DU, indicated on speed ribbon
-- 464, 465

function NGX_SPDREF_MODE_show ()
local txt
local pos = ipc.readLvar('ngx_switch_464_a')
    if pos == 0 then txt ="set"
    elseif pos == 10 then txt = "auto"
    elseif pos == 20 then txt = "v1"
    elseif pos == 30 then txt = "vr"
    elseif pos == 40 then txt = "wt"
    elseif pos == 50 then txt = "vref"
    elseif pos == 60 then txt = "<<<"
    end
    DspShow ("SPDR", txt)
end

function NGX_SPDREF_MODE_calc (sel)
if sel == nil then return end
local pos = ipc.readLvar("L:ngx_switch_464_a")/10
    if pos > sel then
        for i = 1, (pos - sel), 1 do
            NGX_SPDREF_MODE_dec ()
        end
    elseif pos < sel then
        for i = 1, (sel - pos), 1 do
            NGX_SPDREF_MODE_inc ()
        end
    end
end

function NGX_SPDREF_MODE_inc ()
    ipc.control(PMDGBaseVar+464, PMDG_inc)
    NGX_SPDREF_MODE_show ()
end

function NGX_SPDREF_MODE_dec ()
    ipc.control(PMDGBaseVar+464, PMDG_dec)
    NGX_SPDREF_MODE_show ()
end

function NGX_SPDREF_MODE_cycle ()
    if ipc.readLvar("L:ngx_switch_464_a") < 60 then
        ipc.control(PMDGBaseVar+464, PMDG_inc)
        NGX_SPDREF_MODE_show ()
    else
        NGX_SPDREF_MODE_set ()
    end
end

function NGX_SPDREF_MODE_set ()
    if ipc.readLvar("L:ngx_switch_464_a") ~= 0 then
        NGX_SPDREF_MODE_calc (0)
        NGX_SPDREF_MODE_show ()
    end
end

function NGX_SPDREF_MODE_auto ()
    if ipc.readLvar("L:ngx_switch_464_a") ~= 10 then
        NGX_SPDREF_MODE_calc (1)
        NGX_SPDREF_MODE_show ()
    end
end

function NGX_SPDREF_MODE_v1 ()
    if ipc.readLvar("L:ngx_switch_464_a") ~= 20 then
        NGX_SPDREF_MODE_calc (2)
        NGX_SPDREF_MODE_show ()
    end
end

function NGX_SPDREF_MODE_vr ()
    if ipc.readLvar("L:ngx_switch_464_a") ~= 30 then
        NGX_SPDREF_MODE_calc (3)
        NGX_SPDREF_MODE_show ()
    end
end

function NGX_SPDREF_MODE_wt ()
    if ipc.readLvar("L:ngx_switch_464_a") ~= 40 then
        NGX_SPDREF_MODE_calc (4)
        NGX_SPDREF_MODE_show ()
    end
end

function NGX_SPDREF_MODE_vref ()
    if ipc.readLvar("L:ngx_switch_464_a") ~= 50 then
        NGX_SPDREF_MODE_calc (5)
        NGX_SPDREF_MODE_show ()
    end
end

function NGX_SPDREF_MODE_arrow ()
    if ipc.readLvar("L:ngx_switch_464_a") ~= 60 then
        NGX_SPDREF_MODE_calc (6)
        NGX_SPDREF_MODE_show ()
    end
end

-- changes to Speed Reference displayed on
-- pilot's DU

function NGX_SPDREF_mode ()
local txt
local pos = ipc.readLvar('ngx_switch_464_a')
    if pos == 0 then txt ="SET"
    elseif pos == 10 then txt = "AUTO"
    elseif pos == 20 then txt = "V1"
    elseif pos == 30 then txt = "VR"
    elseif pos == 40 then txt = "WT"
    elseif pos == 50 then txt = "VREF"
    elseif pos == 60 then txt = "<<<"
    end
    return txt
end

function NGX_SPDREF_inc ()
local pos
    ipc.control(PMDGBaseVar+465, PMDG_inc)
    pos = NGX_SPDREF_mode()
    DspShow (pos, 'inc')
end

function NGX_SPDREF_dec ()
local pos
    ipc.control(PMDGBaseVar+465, PMDG_dec)
    pos = NGX_SPDREF_mode()
    DspShow (pos, 'dec')
end

-- $$ N1 Set Selector
-- located above centre DU, indicated on speed ribbon
-- 466, 467

function NGX_N1SET_MODE_show ()
local txt
local pos = ipc.readLvar('ngx_switch_466_a')
    if pos == 0 then txt ="2"
    elseif pos == 10 then txt = "1"
    elseif pos == 20 then txt = "auto"
    elseif pos == 30 then txt = "both"
    end
    DspShow ("N1St", txt)
end

function NGX_N1SET_MODE_calc (sel)
if sel == nil then return end
local pos = ipc.readLvar("L:ngx_switch_466_a")/10
    if pos > sel then
        for i = 1, (pos - sel), 1 do
            NGX_N1SET_MODE_dec ()
        end
    elseif pos < sel then
        for i = 1, (sel - pos), 1 do
            NGX_N1SET_MODE_inc ()
        end
    end
end

function NGX_N1SET_MODE_inc ()
    if ipc.readLvar("L:ngx_switch_466_a") < 50 then
        ipc.control(PMDGBaseVar+466, PMDG_inc)
        NGX_N1SET_MODE_show ()
    end
end

function NGX_N1SET_MODE_dec ()
    if ipc.readLvar("L:ngx_switch_466_a") > 0 then
        ipc.control(PMDGBaseVar+466, PMDG_dec)
        NGX_N1SET_MODE_show ()
    end
end

function NGX_N1SET_MODE_cycle ()
    if ipc.readLvar("L:ngx_switch_466_a") < 30 then
        ipc.control(PMDGBaseVar+466, PMDG_inc)
    else
        NGX_N1SET_MODE_2 ()
    end
end

function NGX_N1SET_MODE_2 ()
    if ipc.readLvar("L:ngx_switch_466_a") ~= 0 then
        NGX_N1SET_MODE_calc (0)
        NGX_N1SET_MODE_show ()
    end
end

function NGX_N1SET_MODE_1 ()
    if ipc.readLvar("L:ngx_switch_466_a") ~= 10 then
        NGX_N1SET_MODE_calc (1)
        NGX_N1SET_MODE_show ()
    end
end

function NGX_N1SET_MODE_auto ()
    if ipc.readLvar("L:ngx_switch_466_a") ~= 20 then
        NGX_N1SET_MODE_calc (2)
        NGX_N1SET_MODE_show ()
    end
end

function NGX_N1SET_MODE_both ()
    if ipc.readLvar("L:ngx_switch_466_a") ~= 30 then
        NGX_N1SET_MODE_calc (3)
        NGX_N1SET_MODE_show ()
    end
end

function NGX_N1SET_inc ()
    ipc.control(PMDGBaseVar+467, PMDG_inc)
    DspShow ('N1', ipc.readLvar("L:ngx_switch_467_a"))
end

function NGX_N1SET_dec ()
    ipc.control(PMDGBaseVar+467, PMDG_dec)
    DspShow ('N1', ipc.readLvar("L:ngx_switch_467_a"))
end

-- $$ Fuel Flow Switch
-- 468

function NGX_FUELFLOW_reset ()
    if ipc.readLvar('ngx_switch_468_a') ~= 0 then
        ipc.control(PMDGBaseVar+468, PMDG_ClkR)
        ipc.sleep(250)
        ipc.control(PMDGBaseVar+468, PMDG_RelR)
		DspShow ("FLOW", "rset")
    end
end

function NGX_FUELFLOW_used ()
    if ipc.readLvar('ngx_switch_468_a') ~= 100 then
        ipc.control(PMDGBaseVar+468, PMDG_ClkL)
        ipc.sleep(250)
        ipc.control(PMDGBaseVar+468, PMDG_RelL)
        DspShow ("FLOW", "used")
    end
end

-- ## Warnings ###############

-- $$ Disengage Test Switch

function NGX_AP_TEST_DISENGAGE_2 ()
     if ipc.readLvar('ngx_switch_342_a') ~= 100 then
        ipc.control(PMDGBaseVar+342, PMDG_ClkL)
        ipc.sleep(750)
        ipc.control(PMDGBaseVar+342, PMDG_ClkR)
		DspShow("APds", "2")
    end
end

function NGX_AP_TEST_DISENGAGE_off ()
local pos = ipc.readLvar('ngx_switch_342_a')
    if pos > 50 then
        ipc.control(PMDGBaseVar+342, PMDG_ClkR)
        DspShow("APds", "off")
    elseif pos < 50 then
        ipc.control(PMDGBaseVar+342, PMDG_ClkL)
        DspShow("APds", "off")
    end
end

function NGX_AP_TEST_DISENGAGE_1 ()
     if ipc.readLvar('ngx_switch_342_a') ~= 0 then
        ipc.control(PMDGBaseVar+342, PMDG_ClkR)
        ipc.sleep(750)
        ipc.control(PMDGBaseVar+342, PMDG_ClkL)
		DspShow("APds", "1")
     end
end

-- $$ Warning Resets

function NGX_WARN_FIRE_reset ()
    ipc.control(69979, PMDG_ClkL)
end

function NGX_WARN_MASTER_reset ()
    ipc.control(69980, PMDG_ClkL)
end

function NGX_SYSTEM_ANN_reset ()
    ipc.control(69981, PMDG_ClkL)
end

function NGX_AP_P_reset ()
    ipc.control(69971, PMDG_ClkL)
end

function NGX_AT_P_reset ()
    ipc.control(69972, PMDG_ClkL)
end

function NGX_FMC_P_reset ()
    ipc.control(69973, PMDG_ClkL)
end

function NGX_AP_AT_FMC_reset ()
    ipc.control(69971, PMDG_ClkL)
    ipc.control(69972, PMDG_ClkL)
    ipc.control(69973, PMDG_ClkL)
end

function NGX_BelowGS_Inhibit ()
    ipc.control(69959, PMDG_ClkL)
end

function NGX_WARN_ALL_reset ()
    NGX_WARN_MASTER_reset()
    NGX_WARN_FIRE_reset()
    NGX_AP_P_reset()
    NGX_AT_P_reset()
    NGX_FMC_P_reset()
end

-- ## VHF Panel ###############

function NGX_Marker_show ()
    if ipc.readLvar('ngx_switch_284_a') == 100 then
        DspShow("MKR", " ON")
    elseif ipc.readLvar('ngx_switch_284_a') == 0 then
        DspShow("MKR", "OFF")
    end
end

function NGX_Marker_on ()
    if ipc.readLvar('ngx_switch_284_a') == 100 then
        ipc.control(69916, PMDG_ClkL)
    end
    NGX_Marker_show ()
end

function NGX_Marker_off ()
    if ipc.readLvar('ngx_switch_284_a') == 0 then
        ipc.control(69916, PMDG_ClkL)
    end
    NGX_Marker_show ()
end

-- ## Doors ###############

function DOOR_FWD_L_open ()
    if ipc.readLvar("L:NGXFwdLeftCabinDoor") == 0 then
    	ipc.control(PMDGBaseVar+14005)
    	DspShow ("1 L", "open", "DOOR 1 L", "opening")
    end
end

function DOOR_FWD_L_close ()
    if ipc.readLvar("L:NGXFwdLeftCabinDoor") == 100 then
    	ipc.control(PMDGBaseVar+14005)
    	DspShow ("1 L", "close", "DOOR 1 L", "closing")
    end
end

function DOOR_FWD_L_toggle ()
	if _tl("NGXFwdLeftCabinDoor", 0) then
       DOOR_FWD_L_open ()
	else
       DOOR_FWD_L_close ()
	end
end

---

function DOOR_AFT_L_open ()
    if ipc.readLvar("L:NGXAftLeftCabinDoor") == 0 then
    	ipc.control(PMDGBaseVar+14007)
    	DspShow ("2 L", "open", "DOOR 2 L", "opening")
    end
end

function DOOR_AFT_L_close ()
    if ipc.readLvar("L:NGXAftLeftCabinDoor") == 100 then
    	ipc.control(PMDGBaseVar+14007)
    	DspShow ("2 L", "close", "DOOR 2 L", "closing")
    end
end

function DOOR_AFT_L_toggle ()
	if _tl("NGXAftLeftCabinDoor", 0) then
       DOOR_AFT_L_open ()
	else
       DOOR_AFT_L_close ()
	end
end

function DOOR_L_All_open ()
    DOOR_FWD_L_open ()
    _sleep(200,1500)
    DOOR_AFT_L_open ()
end

function DOOR_L_All_close ()
    DOOR_FWD_L_close ()
    _sleep(200,1500)
    DOOR_AFT_L_close ()
end

function DOOR_FWD_R_open ()
    if ipc.readLvar("L:NGXFwdRightCabinDoor") == 0 then
    	ipc.control(PMDGBaseVar+14006)
    	DspShow ("1 R", "open", "DOOR 1 R", "opening")
    end
end

function DOOR_FWD_R_close ()
    if ipc.readLvar("L:NGXFwdRightCabinDoor") == 100 then
    ipc.control(PMDGBaseVar+14006)
    DspShow ("1 R", "close", "DOOR 1 R", "closing")
    end
end

function DOOR_FWD_R_toggle ()
	if _tl("NGXFwdRightCabinDoor", 0) then
       DOOR_FWD_R_open ()
	else
       DOOR_FWD_R_close ()
	end
end

---

function DOOR_AFT_R_open ()
    if ipc.readLvar("L:NGXAftRightCabinDoor") == 0 then
    	ipc.control(PMDGBaseVar+14008)
    	DspShow ("2 R", "open", "DOOR 2 R", "opening")
    end
end

function DOOR_AFT_R_close ()
    if ipc.readLvar("L:NGXAftRightCabinDoor") == 100 then
    	ipc.control(PMDGBaseVar+14008)
    	DspShow ("2 R", "close", "DOOR 2 R", "closing")
    end
end

function DOOR_AFT_R_toggle ()
	if _tl("NGXAftRightCabinDoor", 0) then
       DOOR_AFT_R_open ()
	else
       DOOR_AFT_R_close ()
	end
end

function DOOR_R_All_open ()
    DOOR_FWD_R_open ()
    _sleep(200,1500)
    DOOR_AFT_R_open ()
end

function DOOR_R_All_close ()
    DOOR_FWD_R_close ()
    _sleep(200,1500)
    DOOR_AFT_R_close ()
end

function CARGO_FWD_open ()
    if ipc.readLvar("L:NGXFwdCargoDoor") == 0 then
    	ipc.control(PMDGBaseVar+14013)
    	DspShow ("Crg1", "open", "CARGO FWD", "opening")
    end
end

function CARGO_FWD_close ()
    if ipc.readLvar("L:NGXFwdCargoDoor") == 100 then
    	ipc.control(PMDGBaseVar+14013)
    	DspShow ("Crg1", "close", "CARGO FWD", "closing")
    end
end

function CARGO_FWD_R_toggle ()
	if _tl("NGXFwdCargoDoor", 0) then
       CARGO_FWD_open ()
	else
       CARGO_FWD_close ()
	end

end

---

function CARGO_AFT_open ()
    if ipc.readLvar("L:NGXAftCargoDoor") == 0 then
    	ipc.control(PMDGBaseVar+14014)
    	DspShow ("Crg2", "open", "CARGO AFT", "opening")
    end
end

function CARGO_AFT_close ()
    if ipc.readLvar("L:NGXAftCargoDoor") == 100 then
    	ipc.control(PMDGBaseVar+14014)
    	DspShow ("Crg2", "close", "CARGO AFT", "closing")
    end
end

function CARGO_AFT_toggle ()
	if _tl("NGXAftCargoDoor", 0) then
       CARGO_AFT_open ()
	else
       CARGO_AFT_close ()
	end
end

function CARGO_All_open ()
    CARGO_FWD_open ()
    _sleep(200,1500)
    CARGO_AFT_open ()
end

function CARGO_All_close ()
    CARGO_FWD_close ()
    _sleep(200,1500)
    CARGO_AFT_close ()
end

-- ## CDU Capt ###############

function NGX_CDU_LSK_1L ()
    ipc.control(70166, PMDG_ClkL)
end

function NGX_CDU_LSK_2L ()
    ipc.control(70167, PMDG_ClkL)
end

function NGX_CDU_LSK_3L ()
    ipc.control(70168, PMDG_ClkL)
end

function NGX_CDU_LSK_4L ()
    ipc.control(70169, PMDG_ClkL)
end

function NGX_CDU_LSK_5L ()
    ipc.control(70170, PMDG_ClkL)
end

function NGX_CDU_LSK_6L ()
    ipc.control(70171, PMDG_ClkL)
end

function NGX_CDU_LSK_1R ()
    ipc.control(70172, PMDG_ClkL)
end

function NGX_CDU_LSK_2R ()
    ipc.control(70173, PMDG_ClkL)
end

function NGX_CDU_LSK_3R ()
    ipc.control(70174, PMDG_ClkL)
end

function NGX_CDU_LSK_4R ()
    ipc.control(70175, PMDG_ClkL)
end

function NGX_CDU_LSK_5R ()
    ipc.control(70176, PMDG_ClkL)
end

function NGX_CDU_LSK_6R ()
    ipc.control(70177, PMDG_ClkL)
end

function NGX_CDU_INIT_REF ()
    ipc.control(70178, PMDG_ClkL)
end

function NGX_CDU_RTE ()
    ipc.control(70179, PMDG_ClkL)
end

function NGX_CDU_CLB ()
    ipc.control(70180, PMDG_ClkL)
end

function NGX_CDU_CRZ ()
    ipc.control(70181, PMDG_ClkL)
end

function NGX_CDU_DES ()
    ipc.control(70182, PMDG_ClkL)
end

function NGX_CDU_MENU ()
    ipc.control(70183, PMDG_ClkL)
end

function NGX_CDU_LEGS ()
    ipc.control(70184, PMDG_ClkL)
end

function NGX_CDU_DEP_ARR ()
    ipc.control(70185, PMDG_ClkL)
end

function NGX_CDU_HOLD ()
    ipc.control(70186, PMDG_ClkL)
end

function NGX_CDU_PROG ()
    ipc.control(70187, PMDG_ClkL)
end

function NGX_CDU_EXEC ()
    ipc.control(70188, PMDG_ClkL)
end

function NGX_CDU_N1_LIMIT ()
    ipc.control(70189, PMDG_ClkL)
end

function NGX_CDU_FIX ()
    ipc.control(70190, PMDG_ClkL)
end

function NGX_CDU_PREV_PAGE ()
    ipc.control(70191, PMDG_ClkL)
end

function NGX_CDU_NEXT_PAGE ()
    ipc.control(70192, PMDG_ClkL)
end

function NGX_CDU_1 ()
    ipc.control(70193, PMDG_ClkL)
end

function NGX_CDU_2 ()
    ipc.control(70194, PMDG_ClkL)
end

function NGX_CDU_3 ()
    ipc.control(70195, PMDG_ClkL)
end

function NGX_CDU_4 ()
    ipc.control(70196, PMDG_ClkL)
end

function NGX_CDU_5 ()
    ipc.control(70197, PMDG_ClkL)
end

function NGX_CDU_6 ()
    ipc.control(70198, PMDG_ClkL)
end

function NGX_CDU_7 ()
    ipc.control(70199, PMDG_ClkL)
end

function NGX_CDU_8 ()
    ipc.control(70200, PMDG_ClkL)
end

function NGX_CDU_9 ()
    ipc.control(70201, PMDG_ClkL)
end

function NGX_CDU_Period ()
    ipc.control(70202, PMDG_ClkL)
end

function NGX_CDU_0 ()
    ipc.control(70203, PMDG_ClkL)
end

function NGX_CDU_Sign ()
    ipc.control(70204, PMDG_ClkL)
end

function NGX_CDU_A ()
    ipc.control(70205, PMDG_ClkL)
end

function NGX_CDU_B ()
    ipc.control(70206, PMDG_ClkL)
end

function NGX_CDU_C ()
    ipc.control(70207, PMDG_ClkL)
end

function NGX_CDU_D ()
    ipc.control(70208, PMDG_ClkL)
end

function NGX_CDU_E ()
    ipc.control(70209, PMDG_ClkL)
end

function NGX_CDU_F ()
    ipc.control(70210, PMDG_ClkL)
end

function NGX_CDU_G ()
    ipc.control(70211, PMDG_ClkL)
end

function NGX_CDU_H ()
    ipc.control(70212, PMDG_ClkL)
end

function NGX_CDU_I ()
    ipc.control(70213, PMDG_ClkL)
end

function NGX_CDU_J ()
    ipc.control(70214, PMDG_ClkL)
end

function NGX_CDU_K ()
    ipc.control(70215, PMDG_ClkL)
end

function NGX_CDU_L ()
    ipc.control(70216, PMDG_ClkL)
end

function NGX_CDU_M ()
    ipc.control(70217, PMDG_ClkL)
end

function NGX_CDU_N ()
    ipc.control(70218, PMDG_ClkL)
end

function NGX_CDU_O ()
    ipc.control(70219, PMDG_ClkL)
end

function NGX_CDU_P ()
    ipc.control(70220, PMDG_ClkL)
end

function NGX_CDU_Q ()
    ipc.control(70221, PMDG_ClkL)
end

function NGX_CDU_R ()
    ipc.control(70222, PMDG_ClkL)
end

function NGX_CDU_S ()
    ipc.control(70223, PMDG_ClkL)
end

function NGX_CDU_T ()
    ipc.control(70224, PMDG_ClkL)
end

function NGX_CDU_U ()
    ipc.control(70225, PMDG_ClkL)
end

function NGX_CDU_V ()
    ipc.control(70226, PMDG_ClkL)
end

function NGX_CDU_W ()
    ipc.control(70227, PMDG_ClkL)
end

function NGX_CDU_X ()
    ipc.control(70228, PMDG_ClkL)
end

function NGX_CDU_Y ()
    ipc.control(70229, PMDG_ClkL)
end

function NGX_CDU_Z ()
    ipc.control(70230, PMDG_ClkL)
end

function NGX_CDU_Space ()
    ipc.control(70231, PMDG_ClkL)
end

function NGX_CDU_DEL ()
    ipc.control(70232, PMDG_ClkL)
end

function NGX_CDU_FwdSlash ()
    ipc.control(70233, PMDG_ClkL)
end

function NGX_CDU_CLR ()
    ipc.control(70234, PMDG_ClkL)
end

-- ## Enter CDU Capt pages ###############

function NGX_CDU_Fuel ()
    NGX_CDU_MENU ()
    ipc.sleep(1)
    NGX_CDU_LSK_5R () -- FS actions
    ipc.sleep(1)
    NGX_CDU_LSK_1L () -- Fuel
    ipc.sleep(1)
    DspShow ("CDU", "Fuel")
end

function NGX_CDU_Payload ()
    NGX_CDU_MENU ()
    ipc.sleep(1)
    NGX_CDU_LSK_5R ()  -- FS actions
    ipc.sleep(1)
    NGX_CDU_LSK_2L ()  -- Payload
    ipc.sleep(1)
    DspShow ("CDU", "Payl")
end

function NGX_CDU_GroundConn ()
    NGX_CDU_MENU ()
    ipc.sleep(1)
    NGX_CDU_LSK_5R ()  -- FS actions
    ipc.sleep(1)
    NGX_CDU_LSK_3L ()  -- Ground connections
    ipc.sleep(10)
    DspShow ("CDU", "Conn")
end

function NGX_CDU_Doors()
    NGX_CDU_MENU ()
    ipc.sleep(10)
    NGX_CDU_LSK_5R ()  -- FS actions
    ipc.sleep(10)
    NGX_CDU_LSK_4L ()  -- Doors
    ipc.sleep(10)
    DspShow ("CDU", "Door")
end

function NGX_CDU_Pushback ()
    NGX_CDU_MENU ()
    ipc.sleep(10)
    NGX_CDU_LSK_5R ()  -- FS actions
    ipc.sleep(10)
    NGX_CDU_LSK_5L ()  -- Push
    ipc.sleep(10)
    DspShow ("CDU", "Conn")
end

-----------------

function NGX_Doors_LeftFWD()
    NGX_CDU_Doors()
    ipc.sleep(10)
    NGX_CDU_LSK_1L ()
    DspShow ("Door", "LFwd")
end

function NGX_Doors_LeftAFT()
    NGX_CDU_Doors()
    ipc.sleep(10)
    NGX_CDU_LSK_2L ()
    DspShow ("Door", "LAft")
end

function NGX_Doors_RightFWD()
    NGX_CDU_Doors()
    ipc.sleep(10)
    NGX_CDU_LSK_1R ()
    DspShow ("Door", "RFwd")
end

function NGX_Doors_RightAFT()
    NGX_CDU_Doors()
    ipc.sleep(10)
    NGX_CDU_LSK_2R ()
    DspShow ("Door", "RAft")
end

function NGX_Doors_CargoFWD()
    NGX_CDU_Doors()
    ipc.sleep(10)
    NGX_CDU_NEXT_PAGE ()
    ipc.sleep(10)
    NGX_CDU_LSK_1L ()
    DspShow ("Crgo", "FWD")
end

function NGX_Doors_CargoAFT()
    NGX_CDU_Doors()
    ipc.sleep(10)
    NGX_CDU_NEXT_PAGE ()
    ipc.sleep(10)
    NGX_CDU_LSK_2L ()
    DspShow ("Crgo", "AFT")
end

function NGX_Doors_Equip()
    NGX_CDU_Doors()
    ipc.sleep(10)
    NGX_CDU_NEXT_PAGE ()
    ipc.sleep(10)
    NGX_CDU_LSK_3L ()
    DspShow ("Eqip", "Htch")
end

function NGX_MainDoors_left ()
    NGX_Doors_LeftFWD()
    _sleep(500, 1000)
    NGX_Doors_LeftAFT()
end

function NGX_MainDoors_right ()
    NGX_Doors_RightFWD()
    _sleep(500, 1000)
    NGX_Doors_RightAFT()
end

function NGX_Cargo_Doors ()
    NGX_Doors_CargoAFT ()
    _sleep(500, 1000)
    NGX_Doors_CargoFWD ()
end

-- ## CDU CAPT Ground Connection ###############



function NGX_CDU1_Wheel_Chocks_on ()

    NGX_CDU_GroundConn ()
    NGXchocks = ipc.readLvar("NGXWheelChocks")

    if NGXchocks == 0 then
    NGX_CDU_LSK_1L ()
    end
    DspShow ("chks", "off", "Chocks", "set")

end

function NGX_CDU1_Wheel_Chocks_off ()

    NGX_CDU_GroundConn ()
    NGXchocks = ipc.readLvar("NGXWheelChocks")

    if NGXchocks == 1 then
    NGX_CDU_LSK_1L ()
    end
    DspShow ("chks", "off", "Chocks", "off")

end

function NGX_CDU1_Wheel_Chocks_toggle ()
	if _tl("NGXWheelChocks", 0) then
       NGX_Wheel_Chocks_on ()
	else
       NGX_Wheel_Chocks_off ()
	end
end

function NGX_CDU1_GroundPower_on ()
    NGX_CDU_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXGPU = ipc.readLvar("NGXGPU")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXGPU == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXGPU == 0 then
        NGX_CDU_LSK_2L ()
    else
    DspShow ("GPU", "on")
    end
end

function NGX_CDU1_GroundPower_off ()
    NGX_CDU_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXGPU = ipc.readLvar("NGXGPU")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXGPU == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXGPU == 1 then
        NGX_CDU_LSK_2L ()
    else
    DspShow ("GPU", "off")
    end
end

function NGX_CDU1_GroundPower_toggle ()
	if _tl("NGXGPU", 0) then
       NGX_GroundPower_on ()
	else
       NGX_GroundPower_off ()
	end
end



function NGX_CDU1_AirStartUnit_on ()
    NGX_CDU_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXair = ipc.readLvar("NGXAirStartCart")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXair == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXair == 0 then
        NGX_CDU_LSK_3L ()
    else
    DspShow ("AIR", "on")
    end
end

function NGX_CDU1_AirStartUnit_off ()
    NGX_CDU_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXair = ipc.readLvar("NGXAirStartCart")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXair == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXair == 1 then
        NGX_CDU_LSK_3L ()
    else
    DspShow ("AIR", "off")
    end
end

function NGX_CDU1_AirStartUnit_toggle ()
	if _tl("NGXAirStartCart", 0) then
      NGX_AirStartUnit_on ()
	else
      NGX_AirStartUnit_off ()
	end
end



function NGX_CDU1_AirConditioningUnit_on ()
    NGX_CDU_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXAC = ipc.readLvar("NGXGSU")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXAC == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXAC == 0 then
        NGX_CDU_LSK_4L ()
    else
    DspShow ("A/C", "on")
    end
end

function NGX_CDU1_AirConditioningUnit_off ()
    NGX_CDU_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXAC = ipc.readLvar("NGXGSU")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXAC == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXAC == 1 then
        NGX_CDU_LSK_4L ()
    else
    DspShow ("A/C", "off")
    end
end

function NGX_CDU1_AirConditioningUnit_toggle ()
	if _tl("NGXGSU", 0) then
      NGX_AirConditioningUnit_on ()
	else
      NGX_AirConditioningUnit_off ()
	end
end



function NGX_CDU1_PitotCovers_on ()
    NGX_CDU_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXPITOT = ipc.readLvar("NGXPitotCovers")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXPITOT == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXPITOT == 0 then
        NGX_CDU_LSK_5L ()
    else
    DspShow ("Pitt", "on", "PitotCvr", "on")
    end
end

function NGX_CDU1_PitotCovers_off ()
    NGX_CDU_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXPITOT = ipc.readLvar("NGXPitotCovers")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXPITOT == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXPITOT == 1 then
        NGX_CDU_LSK_5L ()
    else
    DspShow ("Pitt", "off", "PitotCvr", "off")
    end
end

function NGX_CDU1_PitotCovers_toggle ()
	if _tl("NGXPitotCovers", 0) then
       NGX_CDU1_PitotCovers_on ()
	else
       NGX_CDU1_PitotCovers_off ()
	end
end

function NGX_CDU1_GPU_and_AC_on ()
    NGX_CDU1_GroundPower_on ()
    NGX_CDU1_AirConditioningUnit_on ()

end

function NGX_CDU1_GPU_and_AC_off ()
    NGX_CDU1_GroundPower_off ()
    NGX_CDU1_AirConditioningUnit_off ()
end

function NGX_CDU1_GPU_and_AC_toggle ()
    NGX_CDU1_GroundPower_toggle ()
    NGX_CDU1_AirConditioningUnit_toggle ()
end

function NGX_CDU1_GPU_and_AC_and_AirStart_on ()
    NGX_CDU1_GroundPower_on ()
    NGX_CDU1_AirStartUnit_on ()
    NGX_CDU1_AirConditioningUnit_on ()
end

function NGX_CDU1_GPU_and_AC_and_AirStart_off ()
    NGX_CDU1_GroundPower_off ()
    NGX_CDU1_AirStartUnit_off ()
    NGX_CDU1_AirConditioningUnit_off ()
end

function NGX_CDU1_GPU_and_AC_and_AirStart_toggle ()
    NGX_CDU1_GroundPower_toggle ()
    NGX_CDU1_AirStartUnit_toggle ()
    NGX_CDU1_AirConditioningUnit_toggle ()
end



function NGX_CDU1_All_GroundConnections_on ()
    NGX_CDU1_Wheel_Chocks_on ()
    ipc.sleep(200)
    NGX_CDU1_GroundPower_on ()
    NGX_CDU1_AirStartUnit_on ()
    NGX_CDU1_AirConditioningUnit_on ()
end

function NGX_CDU1_All_GroundConnections_off ()

    NGX_CDU1_GroundPower_off ()
    NGX_CDU1_AirStartUnit_off ()
    NGX_CDU1_AirConditioningUnit_off ()
    ipc.sleep(200)
    NGX_CDU1_Wheel_Chocks_off ()

end



-- ## CDU FO ###############

function NGX_CDU2_LSK_1L ()
    ipc.control(70238, PMDG_ClkL)
end

function NGX_CDU2_LSK_2L ()
    ipc.control(70239, PMDG_ClkL)
end

function NGX_CDU2_LSK_3L ()
    ipc.control(70240, PMDG_ClkL)
end

function NGX_CDU2_LSK_4L ()
    ipc.control(70241, PMDG_ClkL)
end

function NGX_CDU2_LSK_5L ()
    ipc.control(70242, PMDG_ClkL)
end

function NGX_CDU2_LSK_6L ()
    ipc.control(70243, PMDG_ClkL)
end

function NGX_CDU2_LSK_1R ()
    ipc.control(70244, PMDG_ClkL)
end

function NGX_CDU2_LSK_2R ()
    ipc.control(70245, PMDG_ClkL)
end

function NGX_CDU2_LSK_3R ()
    ipc.control(70246, PMDG_ClkL)
end

function NGX_CDU2_LSK_4R ()
    ipc.control(70247, PMDG_ClkL)
end

function NGX_CDU2_LSK_5R ()
    ipc.control(70248, PMDG_ClkL)
end

function NGX_CDU2_LSK_6R ()
    ipc.control(70249, PMDG_ClkL)
end

function NGX_CDU2_INIT_REF ()
    ipc.control(70250, PMDG_ClkL)
end

function NGX_CDU2_RTE ()
    ipc.control(70251, PMDG_ClkL)
end

function NGX_CDU2_CLB ()
    ipc.control(70252, PMDG_ClkL)
end

function NGX_CDU2_CRZ ()
    ipc.control(70253, PMDG_ClkL)
end

function NGX_CDU2_DES ()
    ipc.control(70254, PMDG_ClkL)
end

function NGX_CDU2_MENU ()
    ipc.control(70255, PMDG_ClkL)
end

function NGX_CDU2_LEGS ()
    ipc.control(70256, PMDG_ClkL)
end

function NGX_CDU2_DEP_ARR ()
    ipc.control(70257, PMDG_ClkL)
end

function NGX_CDU2_HOLD ()
    ipc.control(70258, PMDG_ClkL)
end

function NGX_CDU2_PROG ()
    ipc.control(70259, PMDG_ClkL)
end

function NGX_CDU2_EXEC ()
    ipc.control(70260, PMDG_ClkL)
end

function NGX_CDU2_N1_LIMIT ()
    ipc.control(70261, PMDG_ClkL)
end

function NGX_CDU2_FIX ()
    ipc.control(70262, PMDG_ClkL)
end

function NGX_CDU2_PREV_PAGE ()
    ipc.control(70263, PMDG_ClkL)
end

function NGX_CDU2_NEXT_PAGE ()
    ipc.control(70264, PMDG_ClkL)
end

function NGX_CDU2_1 ()
    ipc.control(70265, PMDG_ClkL)
end

function NGX_CDU2_2 ()
    ipc.control(70266, PMDG_ClkL)
end

function NGX_CDU2_3 ()
    ipc.control(70267, PMDG_ClkL)
end

function NGX_CDU2_4 ()
    ipc.control(70268, PMDG_ClkL)
end

function NGX_CDU2_5 ()
    ipc.control(70269, PMDG_ClkL)
end

function NGX_CDU2_6 ()
    ipc.control(70270, PMDG_ClkL)
end

function NGX_CDU2_7 ()
    ipc.control(70271, PMDG_ClkL)
end

function NGX_CDU2_8 ()
    ipc.control(70272, PMDG_ClkL)
end

function NGX_CDU2_9 ()
    ipc.control(70273, PMDG_ClkL)
end

function NGX_CDU2_Period ()
    ipc.control(70274, PMDG_ClkL)
end

function NGX_CDU2_0 ()
    ipc.control(70275, PMDG_ClkL)
end

function NGX_CDU2_Sign ()
    ipc.control(70276, PMDG_ClkL)
end

function NGX_CDU2_A ()
    ipc.control(70277, PMDG_ClkL)
end

function NGX_CDU2_B ()
    ipc.control(70278, PMDG_ClkL)
end

function NGX_CDU2_C ()
    ipc.control(70279, PMDG_ClkL)
end

function NGX_CDU2_D ()
    ipc.control(70280, PMDG_ClkL)
end

function NGX_CDU2_E ()
    ipc.control(70281, PMDG_ClkL)
end

function NGX_CDU2_F ()
    ipc.control(70282, PMDG_ClkL)
end

function NGX_CDU2_G ()
    ipc.control(70283, PMDG_ClkL)
end

function NGX_CDU2_H ()
    ipc.control(70284, PMDG_ClkL)
end

function NGX_CDU2_I ()
    ipc.control(70285, PMDG_ClkL)
end

function NGX_CDU2_J ()
    ipc.control(70286, PMDG_ClkL)
end

function NGX_CDU2_K ()
    ipc.control(70287, PMDG_ClkL)
end

function NGX_CDU2_L ()
    ipc.control(70288, PMDG_ClkL)
end

function NGX_CDU2_M ()
    ipc.control(70289, PMDG_ClkL)
end

function NGX_CDU2_N ()
    ipc.control(70290, PMDG_ClkL)
end

function NGX_CDU2_O ()
    ipc.control(70291, PMDG_ClkL)
end

function NGX_CDU2_P ()
    ipc.control(70292, PMDG_ClkL)
end

function NGX_CDU2_Q ()
    ipc.control(70293, PMDG_ClkL)
end

function NGX_CDU2_R ()
    ipc.control(70294, PMDG_ClkL)
end

function NGX_CDU2_S ()
    ipc.control(70295, PMDG_ClkL)
end

function NGX_CDU2_T ()
    ipc.control(70296, PMDG_ClkL)
end

function NGX_CDU2_U ()
    ipc.control(70297, PMDG_ClkL)
end

function NGX_CDU2_V ()
    ipc.control(70298, PMDG_ClkL)
end

function NGX_CDU2_W ()
    ipc.control(70299, PMDG_ClkL)
end

function NGX_CDU2_X ()
    ipc.control(70300, PMDG_ClkL)
end

function NGX_CDU2_Y ()
    ipc.control(70301, PMDG_ClkL)
end

function NGX_CDU2_Z ()
    ipc.control(70302, PMDG_ClkL)
end

function NGX_CDU2_Space ()
    ipc.control(70303, PMDG_ClkL)
end

function NGX_CDU2_DEL ()
    ipc.control(70304, PMDG_ClkL)
end

function NGX_CDU2_FwdSlash ()
    ipc.control(70305, PMDG_ClkL)
end

function NGX_CDU2_CLR ()
    ipc.control(70306, PMDG_ClkL)
end

-- ## Enter CDU FO pages ###############

function NGX_CDU2_Fuel ()
    NGX_CDU2_MENU ()
    ipc.sleep(10)
    NGX_CDU2_LSK_5R () -- FS actions
    ipc.sleep(10)
    NGX_CDU2_LSK_1L () -- Fuel
    ipc.sleep(10)
    DspShow ("CDU", "Fuel")
end

function NGX_CDU2_Payload ()
    NGX_CDU2_MENU ()
    ipc.sleep(10)
    NGX_CDU2_LSK_5R ()  -- FS actions
    ipc.sleep(10)
    NGX_CDU2_LSK_2L ()  -- Payload
    ipc.sleep(10)
    DspShow ("CDU", "Payl")
end

function NGX_CDU2_GroundConn ()
    NGX_CDU2_MENU ()
    ipc.sleep(10)
    NGX_CDU2_LSK_5R ()  -- FS actions
    ipc.sleep(10)
    NGX_CDU2_LSK_3L ()  -- Ground connections
    ipc.sleep(10)
    DspShow ("CDU", "Conn")
end

function NGX_CDU2_Doors()
    NGX_CDU2_MENU ()
    ipc.sleep(10)
    NGX_CDU2_LSK_5R ()  -- FS actions
    ipc.sleep(10)
    NGX_CDU2_LSK_4L ()  -- Doors
    ipc.sleep(10)
    DspShow ("CDU", "Door")
end

function NGX_CDU2_Pushback ()
    NGX_CDU2_MENU ()
    ipc.sleep(10)
    NGX_CDU2_LSK_5R ()  -- FS actions
    ipc.sleep(10)
    NGX_CDU2_LSK_5L ()  -- Push
    ipc.sleep(10)
    DspShow ("CDU", "Conn")
end

-- ## CDU FO Ground Connection ###############



function NGX_CDU2_Wheel_Chocks_on ()

    NGX_CDU2_GroundConn ()
    NGXchocks = ipc.readLvar("NGXWheelChocks")

    if NGXchocks == 0 then
    NGX_CDU2_LSK_1L ()
    end
    DspShow ("chks", "off", "Chocks", "set")

end

function NGX_CDU2_Wheel_Chocks_off ()

    NGX_CDU2_GroundConn ()
    NGXchocks = ipc.readLvar("NGXWheelChocks")

    if NGXchocks == 1 then
    NGX_CDU2_LSK_1L ()
    end
    DspShow ("chks", "off", "Chocks", "off")

end

function NGX_CDU2_Wheel_Chocks_toggle ()
	if _tl("NGXWheelChocks", 0) then
       NGX_Wheel_Chocks_on ()
	else
       NGX_Wheel_Chocks_off ()
	end
end

function NGX_CDU2_GroundPower_on ()
    NGX_CDU2_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXGPU = ipc.readLvar("NGXGPU")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXGPU == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXGPU == 0 then
        NGX_CDU2_LSK_2L ()
    else
    DspShow ("GPU", "on")
    end
end

function NGX_CDU2_GroundPower_off ()
    NGX_CDU2_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXGPU = ipc.readLvar("NGXGPU")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXGPU == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXGPU == 1 then
        NGX_CDU2_LSK_2L ()
    else
    DspShow ("GPU", "off")
    end
end

function NGX_CDU2_GroundPower_toggle ()
	if _tl("NGXGPU", 0) then
       NGX_GroundPower_on ()
	else
       NGX_GroundPower_off ()
	end
end



function NGX_CDU2_AirStartUnit_on ()
    NGX_CDU2_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXair = ipc.readLvar("NGXAirStartCart")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXair == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXair == 0 then
        NGX_CDU2_LSK_3L ()
    else
    DspShow ("AIR", "on")
    end
end

function NGX_CDU2_AirStartUnit_off ()
    NGX_CDU2_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXair = ipc.readLvar("NGXAirStartCart")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXair == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXair == 1 then
        NGX_CDU2_LSK_3L ()
    else
    DspShow ("AIR", "off")
    end
end

function NGX_CDU2_AirStartUnit_toggle ()
	if _tl("NGXAirStartCart", 0) then
      NGX_AirStartUnit_on ()
	else
      NGX_AirStartUnit_off ()
	end
end



function NGX_CDU2_AirConditioningUnit_on ()
    NGX_CDU2_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXAC = ipc.readLvar("NGXGSU")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXAC == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXAC == 0 then
        NGX_CDU2_LSK_4L ()
    else
    DspShow ("A/C", "on")
    end
end

function NGX_CDU2_AirConditioningUnit_off ()
    NGX_CDU2_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXAC = ipc.readLvar("NGXGSU")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXAC == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXAC == 1 then
        NGX_CDU2_LSK_4L ()
    else
    DspShow ("A/C", "off")
    end
end

function NGX_CDU2_AirConditioningUnit_toggle ()
	if _tl("NGXGSU", 0) then
      NGX_AirConditioningUnit_on ()
	else
      NGX_AirConditioningUnit_off ()
	end
end



function NGX_CDU2_PitotCovers_on ()
    NGX_CDU2_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXPITOT = ipc.readLvar("NGXPitotCovers")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXPITOT == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXPITOT == 0 then
        NGX_CDU2_LSK_5L ()
    else
    DspShow ("Pitt", "on", "PitotCvr", "on")
    end
end

function NGX_CDU2_PitotCovers_off ()
    NGX_CDU2_GroundConn ()
    ipc.sleep(10)
    NGXchocks = ipc.readLvar("NGXWheelChocks")
    NGXPITOT = ipc.readLvar("NGXPitotCovers")


    ipc.sleep(100)
    if NGXchocks == 0 and NGXPITOT == 0 then
        DspShow ("not", "able")
        ipc.sleep(1000)
        DspShow ("set", "chks")
    elseif NGXchocks == 1 and NGXPITOT == 1 then
        NGX_CDU2_LSK_5L ()
    else
    DspShow ("Pitt", "off", "PitotCvr", "off")
    end
end

function NGX_CDU2_PitotCovers_toggle ()
	if _tl("NGXPitotCovers", 0) then
       NGX_CDU2_PitotCovers_on ()
	else
       NGX_CDU2_PitotCovers_off ()
	end
end

function NGX_CDU2_GPU_and_AC_on ()
    NGX_CDU2_GroundPower_on ()
    NGX_CDU2_AirConditioningUnit_on ()

end

function NGX_CDU2_GPU_and_AC_off ()
    NGX_CDU2_GroundPower_off ()
    NGX_CDU2_AirConditioningUnit_off ()
end

function NGX_CDU2_GPU_and_AC_toggle ()
    NGX_CDU2_GroundPower_toggle ()
    NGX_CDU2_AirConditioningUnit_toggle ()
end

function NGX_CDU2_GPU_and_AC_and_AirStart_on ()
    NGX_CDU2_GroundPower_on ()
    NGX_CDU2_AirStartUnit_on ()
    NGX_CDU2_AirConditioningUnit_on ()
end

function NGX_CDU2_GPU_and_AC_and_AirStart_off ()
    NGX_CDU2_GroundPower_off ()
    NGX_CDU2_AirStartUnit_off ()
    NGX_CDU2_AirConditioningUnit_off ()
end

function NGX_CDU2_GPU_and_AC_and_AirStart_toggle ()
    NGX_CDU2_GroundPower_toggle ()
    NGX_CDU2_AirStartUnit_toggle ()
    NGX_CDU2_AirConditioningUnit_toggle ()
end



function NGX_CDU2_All_GroundConnections_on ()
    NGX_CDU2_Wheel_Chocks_on ()
    ipc.sleep(200)
    NGX_CDU2_GroundPower_on ()
    NGX_CDU2_AirStartUnit_on ()
    NGX_CDU2_AirConditioningUnit_on ()
end

function NGX_CDU2_All_GroundConnections_off ()

    NGX_CDU2_GroundPower_off ()
    NGX_CDU2_AirStartUnit_off ()
    NGX_CDU2_AirConditioningUnit_off ()
    ipc.sleep(200)
    NGX_CDU2_Wheel_Chocks_off ()

end


-- ## Cockpit prepare ###############
function NGX_Cockpit_Prepare ()

    ipc.writeLvar("L:yoke_left_hide", 1)
    ipc.writeLvar("L:yoke_right_hide", 1)
    
	NGX_EFIS_BARO_MODE_hpa()
    NGX_EFIS_MINS_MODE_radio()
	
	NGX_WARN_ALL_reset()
	
	NGX_PROBE_HEAT_BOTH_off()
	
	NGX_SIGNS_CHIME_on()
	
	NGX_LAND_ALL_off()
	
end
