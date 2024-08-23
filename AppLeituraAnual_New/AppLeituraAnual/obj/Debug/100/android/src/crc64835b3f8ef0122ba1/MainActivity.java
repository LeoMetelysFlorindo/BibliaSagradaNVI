package crc64835b3f8ef0122ba1;


public class MainActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_btnOC_Click:(Landroid/view/View;)V:__export__\n" +
			"n_btnPL1_Click:(Landroid/view/View;)V:__export__\n" +
			"n_button_Click:(Landroid/view/View;)V:__export__\n" +
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onCreateOptionsMenu:(Landroid/view/Menu;)Z:GetOnCreateOptionsMenu_Landroid_view_Menu_Handler\n" +
			"n_onOptionsItemSelected:(Landroid/view/MenuItem;)Z:GetOnOptionsItemSelected_Landroid_view_MenuItem_Handler\n" +
			"";
		mono.android.Runtime.register ("AppBibliaNVI.MainActivity, AppBibliaNVI", MainActivity.class, __md_methods);
	}


	public MainActivity ()
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("AppBibliaNVI.MainActivity, AppBibliaNVI", "", this, new java.lang.Object[] {  });
	}


	public void btnOrdemRClicked (android.view.View p0)
	{
		n_btnOC_Click (p0);
	}

	private native void n_btnOC_Click (android.view.View p0);


	public void btnPlano1Clicked (android.view.View p0)
	{
		n_btnPL1_Click (p0);
	}

	private native void n_btnPL1_Click (android.view.View p0);


	public void btnSairClicked (android.view.View p0)
	{
		n_button_Click (p0);
	}

	private native void n_button_Click (android.view.View p0);


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public boolean onCreateOptionsMenu (android.view.Menu p0)
	{
		return n_onCreateOptionsMenu (p0);
	}

	private native boolean n_onCreateOptionsMenu (android.view.Menu p0);


	public boolean onOptionsItemSelected (android.view.MenuItem p0)
	{
		return n_onOptionsItemSelected (p0);
	}

	private native boolean n_onOptionsItemSelected (android.view.MenuItem p0);

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
