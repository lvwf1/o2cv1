/**
Jquery easyui datagrid js导出excel
修改自extgrid导出excel
* allows for downloading of grid data (store) directly into excel
* Method: extracts data of gridPanel store, uses columnModel to construct XML excel document,
* converts to Base64, then loads everything into a data URL link.
*
* @author Guo Jing <iTrucks>
* 2015-01-12
*
*/
$.extend($.fn.datagrid.methods, {
    getExcelXml: function (jq, param) {
        var worksheet = this.createWorksheet(jq, param);
        //alert($(jq).datagrid('getColumnFields'));
        var totalWidth = 0;
        var cfs = $(jq).datagrid('getColumnFields');
        /* **判断是否删除第一行以及最后一行************begin****************/
        //console.dir(JSON.stringify(param.enums[0].type));
        if (param.removeFirstCol) {
            if (param.removeLastCol) {
                for (var i = param.removeFirstCol; i < cfs.length - param.removeLastCol; i++) {
                    totalWidth += $(jq).datagrid('getColumnOption', cfs[i]).width;
                }
            }
            else {
                for (var i = param.removeFirstCol; i < cfs.length; i++) {
                    totalWidth += $(jq).datagrid('getColumnOption', cfs[i]).width;
                }
            }
        }
        else {
            if (param.removeLastCol) {
                for (var i = 0; i < cfs.length - param.removeLastCol; i++) {
                    totalWidth += $(jq).datagrid('getColumnOption', cfs[i]).width;
                }
            }
            else {
                for (var i = 0; i < cfs.length; i++) {
                    totalWidth += $(jq).datagrid('getColumnOption', cfs[i]).width;
                }
            }
        }
        /*****************************end*********************************/
        //var totalWidth = this.getColumnModel().getTotalWidth(includeHidden);
        return '<?xml version="1.0" encoding="utf-8"?>' +//xml申明有问题，以修正，注意是utf-8编码，如果是gb2312，需要修改动态页文件的写入编码
    '<ss:Workbook xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns:o="urn:schemas-microsoft-com:office:office">' +
    '<o:DocumentProperties><o:Title>' + param.title + '</o:Title></o:DocumentProperties>' +
    '<ss:ExcelWorkbook>' +
    '<ss:WindowHeight>' + worksheet.height + '</ss:WindowHeight>' +
    '<ss:WindowWidth>' + worksheet.width + '</ss:WindowWidth>' +
    '<ss:ProtectStructure>False</ss:ProtectStructure>' +
    '<ss:ProtectWindows>False</ss:ProtectWindows>' +
    '</ss:ExcelWorkbook>' +
    '<ss:Styles>' +
    '<ss:Style ss:ID="Default">' +
    '<ss:Alignment ss:Vertical="Top"  />' +
    '<ss:Font ss:FontName="arial" ss:Size="10" />' +
    '<ss:Borders>' +
    '<ss:Border  ss:Weight="1" ss:LineStyle="Continuous" ss:Position="Top" />' +
    '<ss:Border  ss:Weight="1" ss:LineStyle="Continuous" ss:Position="Bottom" />' +
    '<ss:Border  ss:Weight="1" ss:LineStyle="Continuous" ss:Position="Left" />' +
    '<ss:Border ss:Weight="1" ss:LineStyle="Continuous" ss:Position="Right" />' +
    '</ss:Borders>' +
    '<ss:Interior />' +
    '<ss:NumberFormat />' +
    '<ss:Protection />' +
    '</ss:Style>' +
    '<ss:Style ss:ID="title">' +
    '<ss:Borders />' +
    '<ss:Font />' +
    '<ss:Alignment  ss:Vertical="Center" ss:Horizontal="Center" />' +
    '<ss:NumberFormat ss:Format="@" />' +
    '</ss:Style>' +
    '<ss:Style ss:ID="headercell">' +
    '<ss:Font ss:Bold="1" ss:Size="10" />' +
    '<ss:Alignment  ss:Horizontal="Center" />' +
    '<ss:Interior ss:Pattern="Solid"  />' +
    '</ss:Style>' +
    '<ss:Style ss:ID="even">' +
    '<ss:Interior ss:Pattern="Solid"  />' +
    '</ss:Style>' +
    '<ss:Style ss:Parent="even" ss:ID="evendate">' +
    '<ss:NumberFormat ss:Format="yyyy-mm-dd" />' +
    '</ss:Style>' +
    '<ss:Style ss:Parent="even" ss:ID="evenint">' +
    '<ss:NumberFormat ss:Format="0" />' +
    '</ss:Style>' +
    '<ss:Style ss:Parent="even" ss:ID="evenfloat">' +
    '<ss:NumberFormat ss:Format="0.00" />' +
    '</ss:Style>' +
    '<ss:Style ss:ID="odd">' +
    '<ss:Interior ss:Pattern="Solid"  />' +
    '</ss:Style>' +
    '<ss:Style ss:Parent="odd" ss:ID="odddate">' +
    '<ss:NumberFormat ss:Format="yyyy-mm-dd" />' +
    '</ss:Style>' +
    '<ss:Style ss:Parent="odd" ss:ID="oddint">' +
    '<ss:NumberFormat ss:Format="0" />' +
    '</ss:Style>' +
    '<ss:Style ss:Parent="odd" ss:ID="oddfloat">' +
    '<ss:NumberFormat ss:Format="0.00" />' +
    '</ss:Style>' +
    '</ss:Styles>' +
    worksheet.xml +
    '</ss:Workbook>';
    },
    createWorksheet: function (jq, param) {
        // Calculate cell data types and extra class names which affect formatting
        var cellType = [];
        var cellTypeClass = [];
        //var cm = this.getColumnModel();
        var totalWidthInPixels = 0;
        var colXml = '';
        var headerXml = '';
        var visibleColumnCountReduction = 0;
        var cfs = $(jq).datagrid('getColumnFields');
        var colCount = cfs.length;
        var cstartIndex = 0, cendIndex = colCount;
        var colWidthEnumsIndex = [];//记录枚举列的下标和枚举数组对应的下标。
        //alert(param.removeFirstCol + "," + param.removeLastCol);
        if (param.removeFirstCol) {
            cstartIndex = param.removeFirstCol;
            if (param.removeLastCol) {
                cendIndex = colCount - param.removeLastCol;
            }
            else {
                cendIndex = colCount;
            }
        }
        else {
            cstartIndex = 0;
            if (param.removeLastCol) {

                cendIndex = colCount - param.removeLastCol;
            }
            else {
                cendIndex = colCount;
            }
        }
        for (var i = cstartIndex; i < cendIndex; i++) {
            if (cfs[i] != '') {
                var w = $(jq).datagrid('getColumnOption', cfs[i]).width;
                totalWidthInPixels += w;
                if (cfs[i] === "") {
                    cellType.push("None");
                    cellTypeClass.push("");
                    ++visibleColumnCountReduction;
                }
                else {
                    colXml += '<ss:Column ss:AutoFitWidth="1" ss:Width="130" />';
                    headerXml += '<ss:Cell ss:StyleID="headercell">' +
                '<ss:Data ss:Type="String">' + $(jq).datagrid('getColumnOption', cfs[i]).title.replace("<br>", "") + '</ss:Data>' +
                '<ss:NamedCell ss:Name="Print_Titles" /></ss:Cell>';
                    cellType.push("String");
                    cellTypeClass.push("");
                    if (param.enums) {
                        //遍历枚举数组，检测是否有列属于枚举类型
                        for (var j = 0; j < param.enums.length; j++) {
                            if ($(jq).datagrid('getColumnOption', cfs[i]).title == param.enums[j].type) {
                                //console.dir($(jq).datagrid('getColumnOption', cfs[i]).title + "," + i);
                                colWidthEnumsIndex.push({ colIndex: i, enumIndex: j });
                            }
                        }
                    }

                }
            }
        }
        //console.dir(JSON.stringify(colWidthEnumsIndex.length));
        var visibleColumnCount = cellType.length - visibleColumnCountReduction;
        var result = {
            height: 9000,
            width: Math.floor(totalWidthInPixels * 30) + 50
        };
        var rows = $(jq).datagrid('getRows');
        // Generate worksheet header details.
        var t = '<ss:Worksheet ss:Name="' + param.title + '">' +
    '<ss:Names>' +
    '<ss:NamedRange ss:Name="Print_Titles" ss:RefersTo="=\'' + param.title + '\'!R1:R2" />' +
    '</ss:Names>' +
    '<ss:Table x:FullRows="1" x:FullColumns="1"' +
    ' ss:ExpandedColumnCount="' + (visibleColumnCount + 2) +
    '" ss:ExpandedRowCount="' + (rows.length + 2) + '">' +
    colXml +
    '<ss:Row ss:AutoFitHeight="1">' +
    headerXml +
    '</ss:Row>';
        // Generate the data rows from the data in the Store
        //for (var i = 0, it = this.store.data.items, l = it.length; i < l; i++) {
        for (var i = 0, it = rows, l = it.length; i < l; i++) {
            t += '<ss:Row>';
            var cellClass = (i & 1) ? 'odd' : 'even';
            r = it[i];
            var k = 0;
            for (var j = cstartIndex; j < cendIndex; j++) {
                //if ((cm.getDataIndex(j) != '')
                if (cfs[j] != '') {
                    //var v = r[cm.getDataIndex(j)];
                    var v = r[cfs[j]];
                    if (cellType[k] !== "None") {
                        t += '<ss:Cell ss:StyleID="' + cellClass + cellTypeClass[k] + '"><ss:Data ss:Type="' + cellType[k] + '">';
                        if (cellType[k] == 'DateTime') {
                            t += v.format('Y-m-d');
                        } else {

                            //判断该列是否属于枚举，如果是，则进行转换
                            for (var lp = 0; lp < colWidthEnumsIndex.length; lp++) {
                                if (j == colWidthEnumsIndex[lp].colIndex) {//当前列是枚举列
                                    //console.dir(JSON.stringify(param.enums[l].value));
                                    //遍历枚举中的值，对元数据进行替换
                                    for (var mp = 0; mp < param.enums[lp].value.length; mp++) {
                                        if (v == param.enums[lp].value[mp].key) {
                                            v = param.enums[lp].value[mp].text;
                                        }
                                        //switch (v) {
                                        //    case param.enums[l].value[m].key:
                                        //        v = param.enums[l].value[m].text;
                                        //        break;
                                        //}
                                    }
                                }
                            }
                            t += v;
                        }
                        t += '</ss:Data></ss:Cell>';
                    }
                    k++;
                }
            }
            t += '</ss:Row>';
        }
        result.xml = t + '</ss:Table>' +
    '<x:WorksheetOptions>' +
    '<x:PageSetup>' +
    '<x:Layout x:CenterHorizontal="1" x:Orientation="Landscape" />' +
    '<x:Footer x:Data="Page &P of &N" x:Margin="0.5" />' +
    '<x:PageMargins x:Top="0.5" x:Right="0.5" x:Left="0.5" x:Bottom="0.8" />' +
    '</x:PageSetup>' +
    '<x:FitToPage />' +
    '<x:Print>' +
    '<x:PrintErrors>Blank</x:PrintErrors>' +
    '<x:FitWidth>1</x:FitWidth>' +
    '<x:FitHeight>32767</x:FitHeight>' +
    '<x:ValidPrinterInfo />' +
    '<x:VerticalResolution>600</x:VerticalResolution>' +
    '</x:Print>' +
    '<x:Selected />' +
    '<x:DoNotDisplayGridlines />' +
    '<x:ProtectObjects>False</x:ProtectObjects>' +
    '<x:ProtectScenarios>False</x:ProtectScenarios>' +
    '</x:WorksheetOptions>' +
    '</ss:Worksheet>';
        return result;
    }
});