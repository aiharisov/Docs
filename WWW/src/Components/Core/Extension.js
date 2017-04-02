class Extension
{
    /** convert string date to date type, 
     * fomrat:"dd.mm.yyyy hh:mm:ss"*/
    static ConvertToDate(date)
    {
        try
        {
            let left, right;
            left = date.split(' ')[0];
            right = date.split(' ')[1]; 
            left = left.split('.')[2] + '-' + left.split('.')[1] + '-' + left.split('.')[0]
            return new Date(left + ' ' + right);
        }
        catch(e)
        {
            return null;
        }
    }
    /** check type and takes a first digit, need for type detection, 
     * default value 0 - 'Undefined'*/
    static GetFirstDigitFromInt(data)
    {
        try {
          data = String(data);
          data = data.substring(0, 1);
          data = Number(data); 
        }
        catch(ex) {
          data = 0;
        }
        return data;
    }
}
export default Extension