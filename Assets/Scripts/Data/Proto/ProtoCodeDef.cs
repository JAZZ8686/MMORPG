using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 协议编号定义
/// </summary>
public class ProtoCodeDef{

	/// <summary>
	/// 发送邮件
	/// </summary>
	public const ushort Mail = 1001;

	/// <summary>
	/// 测试协议
	/// </summary>
	public const ushort Test = 1004;

	/// <summary>
	/// 获取邮件详情
	/// </summary>
	public const ushort Mail_Get_Detail = 1005;

	/// <summary>
	/// 服务器返回邮件列表
	/// </summary>
	public const ushort Mail_Get_List = 1006;

	/// <summary>
	/// 从服务器返回道具列表
	/// </summary>
	public const ushort Mall_Ret_List = 2001;
}
