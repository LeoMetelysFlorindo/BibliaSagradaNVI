package md589596572d28e9e22287dbe8c1cb6e2fa;


public class Apresenta
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("AppLeituraAnual.Apresenta, AppLeituraAnual, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Apresenta.class, __md_methods);
	}


	public Apresenta () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Apresenta.class)
			mono.android.TypeManager.Activate ("AppLeituraAnual.Apresenta, AppLeituraAnual, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
