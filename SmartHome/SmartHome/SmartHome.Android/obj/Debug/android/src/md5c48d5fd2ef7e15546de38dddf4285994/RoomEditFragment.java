package md5c48d5fd2ef7e15546de38dddf4285994;


public class RoomEditFragment
	extends android.app.DialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("SmartHome.Droid.Fragments.RoomEditFragment, SmartHome.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RoomEditFragment.class, __md_methods);
	}


	public RoomEditFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == RoomEditFragment.class)
			mono.android.TypeManager.Activate ("SmartHome.Droid.Fragments.RoomEditFragment, SmartHome.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public RoomEditFragment (java.lang.String p0, java.lang.String p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == RoomEditFragment.class)
			mono.android.TypeManager.Activate ("SmartHome.Droid.Fragments.RoomEditFragment, SmartHome.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);

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
